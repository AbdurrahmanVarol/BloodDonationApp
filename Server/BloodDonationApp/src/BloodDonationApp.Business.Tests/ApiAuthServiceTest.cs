using Microsoft.Extensions.Configuration;
namespace BloodDonationApp.Business.Tests;
public class ApiAuthServiceTest
{
    private IUserRepository _userRepository;
    private IHospitalRepository _hospitalRepository;
    private IBloodGroupRepository _bloodGroupRepository;
    private ICityRepository _cityRepository;
    private IGenderRepository _genderRepository;
    private IUserService _userService;
    private IApiAuthService _apiAuthService;

    [SetUp]
    public void Setup()
    {

        var configuration = new ConfigurationManager();
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"..\BloodDonationApp.API");
        configuration.SetBasePath(path);
        configuration.AddJsonFile("appsettings.json");

        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<BloodDonationAppContext>()
            .UseInMemoryDatabase(databaseName: dbName).Options;
        var context = new BloodDonationAppContext(options);
        _userRepository = new EfUserRepository(context);
        _bloodGroupRepository = new EfBloodGroupRepository(context);
        _cityRepository = new EfCityRepository(context);
        _hospitalRepository = new EfHospitalRepository(context);
        _genderRepository = new EfGenderRepository(context);

        IMapper mapper = new MapperConfiguration(o => o.AddProfile(new Defaultmapper())).CreateMapper();
        var validator = new UserValidator(_cityRepository, _hospitalRepository, _bloodGroupRepository, _genderRepository, _userRepository);
        _userService = new UserService(_userRepository, mapper, validator);
        _apiAuthService = new ApiAuthService(_userService, mapper, configuration);
        EfDbSeeding.SeedDatabase(context);
    }

    [Test]
    public async Task LoginTest()
    {
        var user = new RegisterRequest
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            Password = "test",
            PasswordConfirm = "test",
            UserName = "test",
            BloodGroupId = 1,
            CityId = 1,
            GenderId = 1
        };
        await _apiAuthService.RegisterAsync(user);

        var loginRequest = new LoginRequest
        {
            UserName = "test",
            Password = "test",
            IsKeepLoggedIn = true,
        };

        var userResponse = await _apiAuthService.LoginAsync(loginRequest);

        Assert.Multiple(() =>
        {
            Assert.That(userResponse, Is.Not.Null);
            Assert.That(userResponse.Token, Is.Not.Empty);
            Assert.That(userResponse.UserName, Is.Not.Empty);
            Assert.That(userResponse.BloodGroup, Is.Not.EqualTo(default));
            Assert.That(userResponse.City, Is.Not.EqualTo(default));
            Assert.That(userResponse.Expire, Is.GreaterThan(DateTime.Now.AddDays(4)));
        });
    }

    [Test]
    public async Task LoginWrongUserNameOrPasswordTest()
    {
        var user = new RegisterRequest
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            Password = "test",
            PasswordConfirm = "test",
            UserName = "test",
            BloodGroupId = 1,
            CityId = 1,
            GenderId = 1
        };
        await _apiAuthService.RegisterAsync(user);

        var wrongUserNameRequest = new LoginRequest
        {
            UserName = "test1",
            Password = "test",
            IsKeepLoggedIn = true,
        };

        var wrongPasswordRequest = new LoginRequest
        {
            UserName = "test",
            Password = "test412412",
            IsKeepLoggedIn = true,
        };

        var wrongUserNameAndPasswordRequest = new LoginRequest
        {
            UserName = "test1",
            Password = "test2213123",
            IsKeepLoggedIn = true,
        };

        Assert.Multiple(() =>
        {
            Assert.ThrowsAsync(typeof(ArgumentNullException), async () => { await _apiAuthService.LoginAsync(null); });
            Assert.ThrowsAsync(typeof(ArgumentException), async () => { await _apiAuthService.LoginAsync(wrongUserNameRequest); });
            Assert.ThrowsAsync(typeof(ArgumentException), async () => { await _apiAuthService.LoginAsync(wrongPasswordRequest); });
            Assert.ThrowsAsync(typeof(ArgumentException), async () => { await _apiAuthService.LoginAsync(wrongUserNameAndPasswordRequest); });
        });
    }

}

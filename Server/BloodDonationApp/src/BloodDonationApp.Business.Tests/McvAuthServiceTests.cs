namespace BloodDonationApp.Business.Tests;
public class McvAuthServiceTests
{
    private IUserRepository _userRepository;
    private IHospitalRepository _hospitalRepository;
    private IBloodGroupRepository _bloodGroupRepository;
    private ICityRepository _cityRepository;
    private IGenderRepository _genderRepository;
    private IUserService _userService;
    private IMvcAuthService _mvcAuthService;
    [SetUp]
    public void Setup()
    {
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
        _mvcAuthService = new MvcAuthService(_userService, mapper);
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
        await _mvcAuthService.RegisterAsync(user);

        var loginRequest = new LoginRequest
        {
            UserName = "test",
            Password = "test",
            IsKeepLoggedIn = true,
        };

        var userResponse = await _mvcAuthService.LoginAsync(loginRequest);

        Assert.That(userResponse, Is.Not.Null);
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
        await _mvcAuthService.RegisterAsync(user);

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
            Assert.ThrowsAsync(typeof(ArgumentException), async () => { await _mvcAuthService.LoginAsync(wrongUserNameRequest); });
            Assert.ThrowsAsync(typeof(ArgumentException), async () => { await _mvcAuthService.LoginAsync(wrongPasswordRequest); });
            Assert.ThrowsAsync(typeof(ArgumentException), async () => { await _mvcAuthService.LoginAsync(wrongUserNameAndPasswordRequest); });
        });
    }

}

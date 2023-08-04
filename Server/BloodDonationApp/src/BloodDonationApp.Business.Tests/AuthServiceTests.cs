namespace BloodDonationApp.Business.Tests;
public class AuthServiceTests
{
    private IUserRepository _userRepository;
    private IHospitalRepository _hospitalRepository;
    private IBloodGroupRepository _bloodGroupRepository;
    private ICityRepository _cityRepository;
    private IGenderRepository _genderRepository;
    private IUserService _userService;
    private IAuthService _authService;
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
        _authService = new AuthServiceBase(_userService, mapper);
        EfDbSeeding.SeedDatabase(context);
    }

    [Test]
    public void RegisterTest()
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
        _authService.RegisterAsync(user).GetAwaiter().GetResult();

        var addedUser = _userService.GetByUsernameAsync("test");

        Assert.That(addedUser, Is.Not.Null);
    }

    [Test]
    public void RegisterWithNullRequestTest()
    {
        RegisterRequest user = null;

        Assert.Throws(typeof(ArgumentNullException), () => { _authService.RegisterAsync(user).GetAwaiter().GetResult(); });
    }

    [Test]
    public async Task RegisterWithExistUserNameTest()
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
        await _authService.RegisterAsync(user);

        var user2 = new RegisterRequest
        {
            FirstName = "test2",
            LastName = "test2",
            Email = "test2@test.com",
            Password = "test2",
            PasswordConfirm = "test2",
            UserName = "test",
            BloodGroupId = 1,
            CityId = 1,
            GenderId = 1
        };

        Assert.ThrowsAsync(typeof(ValidationException), async () => { await _authService.RegisterAsync(user2); });
    }

    [Test]
    public void RegisterWithWrongCityIdBloodGroupIdHospitalIdGenderIdTest()
    {
        var wrongBloodGroupIdser = new RegisterRequest
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            Password = "test",
            PasswordConfirm = "test",
            UserName = "test",
            BloodGroupId = 99,
            CityId = 1,
            GenderId = 1
        };
        var wrongCityIdIdser = new RegisterRequest
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            Password = "test",
            PasswordConfirm = "test",
            UserName = "test",
            BloodGroupId = 1,
            CityId = 99,
            GenderId = 1
        };
        var wrongGenderIdser = new RegisterRequest
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            Password = "test",
            PasswordConfirm = "test",
            UserName = "test",
            BloodGroupId = 1,
            CityId = 1,
            GenderId = 99
        };
        var wrongHospitalIdser = new RegisterRequest
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            Password = "test",
            PasswordConfirm = "test",
            UserName = "test",
            BloodGroupId = 1,
            CityId = 1,
            GenderId = 1,
            HospitalId = Guid.NewGuid()
        };

        Assert.Multiple(() =>
        {
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _authService.RegisterAsync(wrongBloodGroupIdser); });
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _authService.RegisterAsync(wrongCityIdIdser); });
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _authService.RegisterAsync(wrongGenderIdser); });
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _authService.RegisterAsync(wrongHospitalIdser); });
        });
    }

    [Test]
    public void RegisterWrongEmailFormatTest()
    {
        var user = new RegisterRequest
        {
            FirstName = "test",
            LastName = "test",
            Email = "test#gmail,com",
            Password = "test",
            PasswordConfirm = "test",
            UserName = "test",
            BloodGroupId = 1,
            CityId = 1,
            GenderId = 1
        };

        Assert.ThrowsAsync(typeof(ValidationException), async () => { await _authService.RegisterAsync(user); });
    }

    [Test]
    public void RegisterUnmatchPasswordAndPasswordConfirmTest()
    {
        var user = new RegisterRequest
        {
            FirstName = "test",
            LastName = "test",
            Email = "test@gmail.com",
            Password = "test",
            PasswordConfirm = "test123",
            UserName = "test",
            BloodGroupId = 1,
            CityId = 1,
            GenderId = 1
        };

        Assert.ThrowsAsync(typeof(ArgumentException), async () => { await _authService.RegisterAsync(user); });
    }

}

namespace BloodDonationApp.Business.Tests;
public class RequestServiceTests
{
    private IUserRepository _userRepository;
    private IRequestRepository _requestRepository;
    private IHospitalRepository _hospitalRepository;
    private IBloodGroupRepository _bloodGroupRepository;
    private ICityRepository _cityRepository;
    private IGenderRepository _genderRepository;
    private IUserService _userService;
    private IRequestService _requestService;
    private Hospital _hospital;

    [SetUp]
    public async Task Setup()
    {
        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<BloodDonationAppContext>()
            .UseInMemoryDatabase(databaseName: dbName).Options;
        var context = new BloodDonationAppContext(options);
        _userRepository = new EfUserRepository(context);
        _requestRepository = new EfRequestRepository(context);
        _bloodGroupRepository = new EfBloodGroupRepository(context);
        _cityRepository = new EfCityRepository(context);
        _hospitalRepository = new EfHospitalRepository(context);
        _genderRepository = new EfGenderRepository(context);

        IMapper mapper = new MapperConfiguration(o => o.AddProfile(new Defaultmapper())).CreateMapper();
        var userValidator = new UserValidator(_cityRepository, _hospitalRepository, _bloodGroupRepository, _genderRepository, _userRepository);
        var hospitalValidator = new HospitalValidator(_cityRepository);
        var requestValidator = new RequestValidator(_bloodGroupRepository, _hospitalRepository);

        _userService = new UserService(_userRepository, mapper, userValidator);
        _requestService = new RequestService(_requestRepository, _userService, mapper, requestValidator);
        EfDbSeeding.SeedDatabase(context);

        _hospital = (await _hospitalRepository.GetAllAsync()).First();
    }

    [Test]
    public async Task CreateHospitalTest()
    {
        var request = new CreateRequestRequest
        {
            BloodGroupId = 1,
            Quantity = 1,
            HospitalId = _hospital.Id
        };
        var id = await _requestService.AddAsync(request);

        var hasRequest = await _requestRepository.IsExistAsync(p => p.Id == id);

        Assert.That(hasRequest, Is.True);
    }

    [Test]
    public void CreateHospitalWithWrongParametersTest()
    {
        var wrongBloodGroupIdRequest = new CreateRequestRequest
        {
            BloodGroupId = 99,
            Quantity = 1,
            HospitalId = _hospital.Id,
        };

        var unexistHospitalIdRequest = new CreateRequestRequest
        {
            BloodGroupId = 1,
            Quantity = 1,
            HospitalId = Guid.NewGuid()
        };

        Assert.Multiple(() =>
        {
            Assert.ThrowsAsync(typeof(ArgumentNullException), async () => { await _requestService.AddAsync(null); });
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _requestService.AddAsync(wrongBloodGroupIdRequest); });
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _requestService.AddAsync(unexistHospitalIdRequest); });
        });
    }


    [Test]
    public async Task RequestGetByIdTest()
    {
        var request = new CreateRequestRequest
        {
            BloodGroupId = 3,
            Quantity = 1,
            HospitalId = _hospital.Id,
        };
        var id = await _requestService.AddAsync(request);

        var addedRequest = await _requestService.GetByIdAsync(id);

        Assert.Multiple(() =>
        {
            Assert.That(addedRequest, Is.Not.Null);
            Assert.That(addedRequest?.Quantity, Is.EqualTo(request.Quantity));
            Assert.That(addedRequest?.BloodGroupId, Is.EqualTo(request.BloodGroupId));
            Assert.That(addedRequest?.HospitalId, Is.EqualTo(request.HospitalId));
        });
    }

    [Test]
    public async Task RequestGetByBloodGroupIdTest()
    {
        var request = new CreateRequestRequest
        {
            BloodGroupId = 1,
            Quantity = 1,
            HospitalId = _hospital.Id
        };

        var request2 = new CreateRequestRequest
        {
            BloodGroupId = 2,
            Quantity = 1,
            HospitalId = _hospital.Id
        };

        var id = await _requestService.AddAsync(request);
        var id2 = await _requestService.AddAsync(request2);

        var requests = await _requestService.GetRequestsByBloodGroupIdAsync(1);

        var hasAddedRequest = requests.Any(p => p.Id == id);

        Assert.Multiple(() =>
        {
            Assert.That(requests, Is.Not.Null);
            Assert.That(requests.Count(), Is.GreaterThan(0));
            Assert.That(hasAddedRequest, Is.True);
        });
    }

    [Test]
    public async Task RequestGetRequestsTest()
    {
        var request = new CreateRequestRequest
        {
            BloodGroupId = 1,
            Quantity = 1,
            HospitalId = _hospital.Id,
        };
        var id = await _requestService.AddAsync(request);


        var requests = await _requestService.GetRequestsAsync();

        var hasAddedRequest = requests.Any(p => p.Id == id);


        Assert.Multiple(() =>
        {
            Assert.That(requests, Is.Not.Null);
            Assert.That(hasAddedRequest, Is.True);
        });
    }

    [Test]
    public async Task RequestDeleteTest()
    {
        var request = new CreateRequestRequest
        {
            BloodGroupId = 1,
            Quantity = 1,
            HospitalId = _hospital.Id,
        };
        var id = await _requestService.AddAsync(request);

        await _requestService.DeleteAsync(id);

        var requests = await _requestService.GetRequestsAsync();

        var hasAddedRequest = requests.Any(p => p.Id == id);

        Assert.That(hasAddedRequest, Is.False);
    }

    [Test]
    public async Task RequestUpdateTest()
    {
        var request = new CreateRequestRequest
        {
            BloodGroupId = 1,
            Quantity = 1,
            HospitalId = _hospital.Id
        };
        var id = await _requestService.AddAsync(request);

        var updateRequest = new UpdateRequestRequest
        {
            Id = id,
            BloodGroupId = 2,
            Quantity = 3,
            HospitalId = _hospital.Id
        };

        await _requestService.UpdateAsync(updateRequest);

        var updatedRequest = await _requestService.GetByIdAsync(id);

        Assert.Multiple(() =>
        {
            Assert.That(updatedRequest, Is.Not.Null);
            Assert.That(updatedRequest?.Quantity, Is.EqualTo(updateRequest.Quantity));
            Assert.That(updatedRequest?.BloodGroupId, Is.EqualTo(updateRequest.BloodGroupId));
            Assert.That(updatedRequest?.HospitalId, Is.EqualTo(updateRequest.HospitalId));

        });
    }

}

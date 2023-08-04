namespace BloodDonationApp.Business.Tests;
public class HospitalServiceTests
{
    private IUserRepository _userRepository;
    private IHospitalRepository _hospitalRepository;
    private IBloodGroupRepository _bloodGroupRepository;
    private ICityRepository _cityRepository;
    private IGenderRepository _genderRepository;
    private IUserService _userService;
    private IHospitalService _hospitalService;

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
        var userValidator = new UserValidator(_cityRepository, _hospitalRepository, _bloodGroupRepository, _genderRepository, _userRepository);
        var hospitalValidator = new HospitalValidator(_cityRepository);
        _userService = new UserService(_userRepository, mapper, userValidator);
        _hospitalService = new HospitalService(_hospitalRepository, mapper, hospitalValidator, _userService);
        EfDbSeeding.SeedDatabase(context);
    }

    [Test]
    public async Task CreateHospitalTest()
    {
        var hospital = new CreateHospitalRequest
        {
            CityId = 1,
            Name = "Test",
            PhoneNumber = "+90(548)123-45-78",
            Address = "address"
        };
        var id = await _hospitalService.AddAsync(hospital);

        var hasHospital = await _hospitalService.HasHospitalAsync(id);

        Assert.That(hasHospital, Is.True);
    }

    [Test]
    public void CreateHospitalWithWrongParametersTest()
    {
        var wrongCityIdRequest = new CreateHospitalRequest
        {
            CityId = 199,
            Name = "Test",
            PhoneNumber = "+90(548)123-45-78",
            Address = "address"
        };

        var emptyNameRequest = new CreateHospitalRequest
        {
            CityId = 1,
            Name = "",
            PhoneNumber = "+90(548)123-45-78",
            Address = "address"
        };

        var wrongPhoneFormatRequest = new CreateHospitalRequest
        {
            CityId = 1,
            Name = "Test",
            PhoneNumber = "123456789",
            Address = "address"
        };

        var emptyAddress = new CreateHospitalRequest
        {
            CityId = 1,
            Name = "Test",
            PhoneNumber = "+90(548)123-45-78",
            Address = ""
        };

        Assert.Multiple(() =>
        {
            Assert.ThrowsAsync(typeof(ArgumentNullException), async () => { await _hospitalService.AddAsync(null); });
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _hospitalService.AddAsync(wrongCityIdRequest); });
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _hospitalService.AddAsync(emptyNameRequest); });
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _hospitalService.AddAsync(wrongPhoneFormatRequest); });
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _hospitalService.AddAsync(emptyAddress); });
        });
    }

    [Test]
    public async Task HospitalGetByIdTest()
    {
        var hospital = new CreateHospitalRequest
        {
            CityId = 1,
            Name = "Test",
            PhoneNumber = "+90(548)123-45-78",
            Address = "address"
        };
        var id = await _hospitalService.AddAsync(hospital);

        var addedHospital = await _hospitalService.GetByIdAsync(id);

        Assert.Multiple(() =>
        {
            Assert.That(addedHospital, Is.Not.Null);
            Assert.That(addedHospital?.Name, Is.EqualTo(hospital.Name));
            Assert.That(addedHospital?.Address, Is.EqualTo(hospital.Address));
            Assert.That(addedHospital?.PhoneNumber, Is.EqualTo(hospital.PhoneNumber));
            Assert.That(addedHospital?.CityId, Is.EqualTo(hospital.CityId));
        });
    }

    [Test]
    public async Task HospitalGetHospitalsTest()
    {
        var hospital = new CreateHospitalRequest
        {
            CityId = 1,
            Name = "Test",
            PhoneNumber = "+90(548)123-45-78",
            Address = "address"
        };
        var id = await _hospitalService.AddAsync(hospital);

        var hospitals = await _hospitalService.GetHospitalsAsync();

        var hasAddedHospital = hospitals.Any(p => p.Id == id);

        var count = hospitals.Count();

        Assert.Multiple(() =>
        {
            Assert.That(hospitals, Is.Not.Null);
            Assert.That(hasAddedHospital, Is.True);
        });
    }

    [Test]
    public async Task HospitalDeleteTest()
    {
        var hospital = new CreateHospitalRequest
        {
            CityId = 1,
            Name = "Test",
            PhoneNumber = "+90(548)123-45-78",
            Address = "address"
        };
        var id = await _hospitalService.AddAsync(hospital);

        await _hospitalService.DeleteAsync(id);

        var hasHospital = await _hospitalService.HasHospitalAsync(id);

        Assert.That(hasHospital, Is.False);
    }

    [Test]
    public async Task HospitalUpdateTest()
    {
        var hospital = new CreateHospitalRequest
        {
            CityId = 1,
            Name = "Test",
            PhoneNumber = "+90(548)123-45-78",
            Address = "address"
        };
        var id = await _hospitalService.AddAsync(hospital);

        var updateRequest = new UpdateHospitalRequest
        {
            Id = id,
            CityId = 1,
            Name = "Test1",
            Address = "address1",
            PhoneNumber = "+90(548)123-45-55",
        };

        await _hospitalService.UpdateAsync(updateRequest);

        var updatedHospital = await _hospitalService.GetByIdAsync(id);

        Assert.Multiple(() =>
        {
            Assert.That(updatedHospital, Is.Not.Null);
            Assert.That(updatedHospital?.Name, Is.EqualTo(updateRequest.Name));
            Assert.That(updatedHospital?.Address, Is.EqualTo(updateRequest.Address));
            Assert.That(updatedHospital?.PhoneNumber, Is.EqualTo(updateRequest.PhoneNumber));
            Assert.That(updatedHospital?.CityId, Is.EqualTo(updateRequest.CityId));

        });
    }

    [Test]
    public async Task HospitalUpdateWithWrongParametersTest()
    {
        var hospital = new CreateHospitalRequest
        {
            CityId = 1,
            Name = "Test",
            PhoneNumber = "+90(548)123-45-78",
            Address = "address"
        };

        var id = await _hospitalService.AddAsync(hospital);

        var wrongCityIdRequest = new UpdateHospitalRequest
        {
            Id = id,
            CityId = 999,
            Name = "Test1",
            Address = "address1",
            PhoneNumber = "+90(548)123-45-55",
        };

        var emptyNameRequest = new UpdateHospitalRequest
        {
            Id = id,
            CityId = 1,
            Name = "",
            Address = "address1",
            PhoneNumber = "+90(548)123-45-55",
        };

        var wrongPhoneFormatRequest = new UpdateHospitalRequest
        {
            Id = id,
            CityId = 1,
            Name = "Test1",
            Address = "address1",
            PhoneNumber = "05421598751",
        };

        var emptyAddressRequest = new UpdateHospitalRequest
        {
            Id = id,
            CityId = 1,
            Name = "Test1",
            Address = "",
            PhoneNumber = "+90(548)123-45-55",
        };

        var unexitIdRequest = new UpdateHospitalRequest
        {
            Id = Guid.NewGuid(),
            CityId = 1,
            Name = "Test1",
            Address = "address1",
            PhoneNumber = "+90(548)123-45-55",
        };

        Assert.Multiple(() =>
        {
            Assert.ThrowsAsync(typeof(ArgumentNullException), async () => { await _hospitalService.UpdateAsync(null); });
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _hospitalService.UpdateAsync(wrongCityIdRequest); });
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _hospitalService.UpdateAsync(emptyNameRequest); });
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _hospitalService.UpdateAsync(wrongPhoneFormatRequest); });
            Assert.ThrowsAsync(typeof(ValidationException), async () => { await _hospitalService.UpdateAsync(emptyAddressRequest); });
            Assert.ThrowsAsync(typeof(ArgumentException), async () => { await _hospitalService.UpdateAsync(unexitIdRequest); });
        });
    }

    [Test]
    public async Task HospitalAddEmployeeTest()
    {
        var hospital = new CreateHospitalRequest
        {
            CityId = 1,
            Name = "Test",
            PhoneNumber = "+90(548)123-45-78",
            Address = "address"
        };

        var id = await _hospitalService.AddAsync(hospital);

        var user = (await _userRepository.GetAllAsync()).First();

        var addEmployeeRequest = new AddEmployeeRequest
        {
            EmployeeId = user.Id,
            HospitalId = id
        };

        await _hospitalService.AddEmployeeAsync(addEmployeeRequest);

        var employees = await _userRepository.GetAllAsync(p => p.HospitalId == id && p.RoleId == (int)Roles.Staff);

        var hasAddedEmployee = employees.Any(p => p.Id == user.Id);

        Assert.Multiple(() =>
        {
            Assert.That(employees, Is.Not.Null);
            Assert.That(hasAddedEmployee, Is.True);
        });
    }

    [Test]
    public async Task HospitalRemoveEmployeeTest()
    {
        var hospital = new CreateHospitalRequest
        {
            CityId = 1,
            Name = "Test",
            PhoneNumber = "+90(548)123-45-78",
            Address = "address"
        };

        var id = await _hospitalService.AddAsync(hospital);

        var user = (await _userRepository.GetAllAsync()).First();

        var addEmployeeRequest = new AddEmployeeRequest
        {
            EmployeeId = user.Id,
            HospitalId = id
        };

        await _hospitalService.AddEmployeeAsync(addEmployeeRequest);

        var removeEmployeeRequest = new RemoveEmployeeRequest
        {
            EmployeeId = user.Id,
            HospitalId = id
        };

        await _hospitalService.RemoveEmployeeAsync(removeEmployeeRequest);

        var employees = await _userRepository.GetAllAsync(p => p.HospitalId == id && p.RoleId == (int)Roles.Staff);

        var hasAddedEmployee = employees.Any(p => p.Id == user.Id);

        Assert.That(hasAddedEmployee, Is.False);

    }

}

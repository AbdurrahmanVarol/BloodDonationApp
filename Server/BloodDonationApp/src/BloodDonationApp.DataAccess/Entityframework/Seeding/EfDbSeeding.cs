using BloodDonationApp.DataAccess.Entityframework.Contexts;
using BloodDonationApp.Entities.Entities;
using BloodDonationApp.Entities.Enums;

namespace BloodDonationApp.DataAccess.Entityframework.Seeding;
public class EfDbSeeding
{
    public static void SeedDatabase(BloodDonationAppContext context)
    {
        SeedCityIfNoExists(context);
        SeedRolesIfNoExists(context);
        SeedBloodGroupIfNoExists(context);
        SeedGenderIfNoExists(context);
        SeedHospitalIfNoExists(context);
        SeedUserIfNoExists(context);
    }
    private static void SeedUserIfNoExists(BloodDonationAppContext context)
    {
        if (!context.Users.Any())
        {
            //password:12345
            var passwordSalt = "8qjYoxBQ2SgvH7vcbDsPbus2YFpicja5cDbz9IL6hJIgS4gTgr5uq1ADDLy7GHsIEY+0otBju+h74HRuNuFnU25/HWCXOjdKqPlksusj7mNjAR6rk9K9Oy4s1wIySzCoy3xi205Kqhgb4NJ0UcryFCvT6G/9QDQ63A9NyNVQ8s0=";
            var passwordHash = "WMA4dhrMhW2ZW3+8wIlpzcew0pVATmgSq4WZ+tjmiOW1R09J5lKdcxR16RIT1ds44FjeYM0o+ksAeTzSX6aXZQ==";

            var hospital = context.Hospitals.First();
            var users = new List<User>() {
                        new User
                        {
                            FirstName = "Abdurrahman",
                            LastName = "Varol",
                            Email = "abdurrahman@gmail.com",
                            UserName = "abdurrahman",
                            PasswordSalt = passwordSalt,
                            PasswordHash = passwordHash,
                            BloodGroupId = 1,
                            CityId = 58,
                            GenderId = 1,
                            RoleId = (int)Roles.Admin,
                        },
                         new User
                        {
                            FirstName = "Faruk",
                            LastName = "Far",
                            Email = "farukfar@gmail.com",
                            UserName = "faruk",
                            PasswordSalt = passwordSalt,
                            PasswordHash = passwordHash,
                            BloodGroupId = 2,
                            CityId = 58,
                            GenderId = 1,
                            RoleId = (int)Roles.Staff,
                            HospitalId = hospital.Id,
                        },
                          new User
                        {
                            FirstName = "Kazım",
                            LastName = "Kaz",
                            Email = "kazimkaz@gmail.com",
                            UserName = "kazim",
                            PasswordSalt = passwordSalt,
                            PasswordHash = passwordHash,
                            BloodGroupId = 3,
                            CityId = 58,
                            GenderId = 1,
                            RoleId = (int)Roles.Donor,
                        }
            };
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }

    private static void SeedCityIfNoExists(BloodDonationAppContext context)
    {
        if (!context.Cities.Any())
        {
            var cities = new List<City>
           {
               new City{Name="Adana",Plate="01"},
               new City{Name="Adıyaman",Plate="02"},
                new City{Name="Afyonkarahisar",Plate="03"},
                new City{Name="Ağrı",Plate="04"},
                new City{Name="Aksaray",Plate="68"},
                new City{Name="Amasya",Plate="05"},
                new City{Name="Ankara",Plate="06"},
                new City{Name="Antalya",Plate="07"},
                new City{Name="Ardahan",Plate="75"},
                new City{Name="Artvin",Plate="08"},
                new City{Name="Aydın",Plate="09"},
                new City{Name="Balıkesir",Plate="10"},
                new City{Name="Bartın",Plate="74"},
                new City{Name="Batman",Plate="72"},
                new City{Name="Bayburt",Plate="69"},
                new City{Name="Bilecik",Plate="11"},
                new City{Name="Bingöl",Plate="12"},
                new City{Name="Bitlis",Plate="13"},
                new City{Name="Bolu",Plate="14"},
                new City{Name="Burdur",Plate="15"},
                new City{Name="Bursa",Plate="16"},
                new City{Name="Çanakkale",Plate="17"},
                new City{Name="Çankırı",Plate="18"},
                new City{Name="Çorum",Plate="19"},
                new City{Name="Denizli",Plate="20"},
                new City{Name="Diyarbakır",Plate="21"},
                new City{Name="Düzce",Plate="81"},
                new City{Name="Edirne",Plate="22"},
                new City{Name="Elazığ",Plate="23"},
                new City{Name="Erzincan",Plate="24"},
                new City{Name="Erzurum",Plate="25"},
                new City{Name="Eskişehir",Plate="26"},
                new City{Name="Gaziantep",Plate="27"},
                new City{Name="Giresun",Plate="28"},
                new City{Name="Gümüşhane",Plate="29"},
                new City{Name="Hakkâri",Plate="30"},
                new City{Name="Hatay",Plate="31"},
                new City{Name="Iğdır",Plate="76"},
                new City{Name="Isparta",Plate="32"},
                new City{Name="İstanbul",Plate="34"},
                new City{Name="İzmir",Plate="35"},
                new City{Name="Kahramanmaraş",Plate="46"},
                new City{Name="Karabük",Plate="78"},
                new City{Name="Karaman",Plate="70"},
                new City{Name="Kars",Plate="36"},
                new City{Name="Kastamonu",Plate="37"},
                new City{Name="Kayseri",Plate="38"},
                new City{Name="Kırıkkale",Plate="71"},
                new City{Name="Kırklareli",Plate="39"},
                new City{Name="Kırşehir",Plate="40"},
                new City{Name="Kilis",Plate="79"},
                new City{Name="Kocaeli",Plate="41"},
                new City{Name="Konya",Plate="42"},
                new City{Name="Kütahya",Plate="43"},
                new City{Name="Malatya",Plate="44"},
                new City{Name="Manisa",Plate="45"},
                new City{Name="Mardin",Plate="47"},
                new City{Name="Mersin",Plate="33"},
                new City{Name="Muğla",Plate="48"},
                new City{Name="Muş",Plate="49"},
                new City{Name="Nevşehir",Plate="50"},
                new City{Name="Niğde",Plate="51"},
                new City{Name="Ordu",Plate="52"},
                new City{Name="Osmaniye",Plate="80"},
                new City{Name="Rize",Plate="53"},
                new City{Name="Sakarya",Plate="54"},
                new City{Name="Samsun",Plate="55"},
                new City{Name="Siirt",Plate="56"},
                new City{Name="Sinop",Plate="57"},
                new City{Name="Sivas",Plate="58"},
                new City{Name="Şanlıurfa",Plate="63"},
                new City{Name="Şırnak",Plate="73"},
                new City{Name="Tekirdağ",Plate="59"},
                new City{Name="Tokat",Plate="60"},
                new City{Name="Trabzon",Plate="61"},
                new City{Name="Tunceli",Plate="62"},
                new City{Name="Uşak",Plate="64"},
                new City{Name="Van",Plate="65"},
                new City{Name="Yalova",Plate="77"},
                new City{Name="Yozgat",Plate="66"},
                new City{Name="Zonguldak",Plate="67"}
           };
            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }

    private static void SeedBloodGroupIfNoExists(BloodDonationAppContext context)
    {
        if (!context.BloodGroups.Any())
        {
            var bloodGroups = new List<BloodGroup>
            {
                new BloodGroup
                {
                    Name="A Rh Pozitif",
                    Symbol = "A+"
                },
                new BloodGroup
                {
                    Name="A Rh Negatif",
                    Symbol = "A-"
                },
                new BloodGroup
                {
                    Name="B Rh Pozitif",
                    Symbol = "B+"
                },
                new BloodGroup
                {
                    Name="B Rh Negatif",
                    Symbol = "B-"
                },
                new BloodGroup
                {
                    Name="AB Rh Pozitif",
                    Symbol = "AB+"
                },
                new BloodGroup
                {
                    Name="AB Rh Negatif",
                    Symbol = "AB-"
                },
                new BloodGroup
                {
                    Name="0 Rh Pozitif",
                    Symbol = "0+"
                },
                new BloodGroup
                {
                    Name="0 Rh Negatif",
                    Symbol = "0-"
                }

            };
            context.BloodGroups.AddRange(bloodGroups);
            context.SaveChanges();
        }
    }

    private static void SeedHospitalIfNoExists(BloodDonationAppContext context)
    {
        if (!context.Hospitals.Any())
        {
            var hospitals = new List<Hospital>
           {
              new Hospital
              {
                  CityId = 58,
                  Name = "Mersin Şehir Hastanesi",
                  Address = "Korukent Mah. 96015 Sok. Mersin Entegre Sağlık Kampüsü, 33240 Toroslar/Mersin",
                  PhoneNumber = "+90(324)225-10-00"
              },
              new Hospital
              {
                  CityId = 58,
                  Name = "Mersin Toros Devlet Hastanesi",
                  Address = "Mesudiye, 5117. Sk. No:34, 33060 Akdeniz/Mersin",
                  PhoneNumber = "+90(324)233-71-80"
              },
           };
            context.Hospitals.AddRange(hospitals);
            context.SaveChanges();
        }
    }

    private static void SeedRolesIfNoExists(BloodDonationAppContext context)
    {
        if (!context.Roles.Any())
        {
            var roles = new List<Role>
           {
              new Role
              {
                  Name="Admin"
              },
              new Role
              {
                  Name="Staff"
              },
              new Role
              {
                  Name="Donor"
              }
           };
            context.Roles.AddRange(roles);
            context.SaveChanges();
        }
    }
    private static void SeedGenderIfNoExists(BloodDonationAppContext context)
    {
        if (!context.Genders.Any())
        {
            var genders = new List<Gender>
           {
              new Gender
              {
                  Name="Erkek"
              },
              new Gender
              {
                  Name="Kadın"
              }
           };
            context.Genders.AddRange(genders);
            context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]

    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allInfo;
        private string allNames;

        public ContactData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public ContactData()
        {
            FirstName = "";
            LastName = "";
        }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "firstname")]
        public string FirstName { get; set; }

        [Column(Name = "lastname")]
        public string LastName { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        public string AllPhones { 
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUpPhoneNumber(HomePhone) + CleanUpPhoneNumber(MobilePhone) + CleanUpPhoneNumber(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        [Column(Name = "email")]
        public string FirstEmail{ get; set; }

        [Column(Name = "email2")]
        public string SecondEmail { get; set; }

        [Column(Name = "email3")]
        public string ThirdEmail { get; set; }

        public string AllEmails {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return FirstEmail + SecondEmail + ThirdEmail;
                }
            }
            set
            {
                allEmails = value;
            }

        }

        public string AllNames
        {
            get
            {
                if (allNames != null)
                {
                    return allNames;
                }
                else if (String.IsNullOrEmpty(FirstName) && String.IsNullOrEmpty(LastName))
                {
                    return "";
                }
                else if (String.IsNullOrEmpty(FirstName))
                {
                    return LastName;
                }
                else if (String.IsNullOrEmpty(LastName))
                {
                    return FirstName;
                }
                else
                {
                    return FirstName + " " + LastName;
                }
            }
            set
            {
                allNames = value;
            }
        }

        public string AllInfo
        {
            get
            {
                if (allInfo != null)
                {
                    return allInfo;
                }
                else
                {
                    return (DetailPerson() + DetailPhoneNumbers() + DetailEmails()).Trim();
                }
            }
            set
            {
                allInfo = value;
            }
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return 0;
            }

            return LastName.CompareTo(other.LastName);

        }


        public override int GetHashCode()
        {
            return FirstName.GetHashCode() + LastName.GetHashCode();
        }

        public override string ToString()
        {
            return "Firstname = " + FirstName + ", Lastname = " + LastName;
        }

        private string CleanUpPhoneNumber(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ ()]|-", "") + "\r\n";
        }

        
        private string DetailPhoneNumbers()
        {
            string finalNumberString = "\r\n";

            if (HomePhone != null && HomePhone != "")
            {
                finalNumberString += "H: " + HomePhone + "\r\n";
            }

            if (MobilePhone != null && MobilePhone != "")
            {
                finalNumberString += "M: " + MobilePhone + "\r\n";
            }

            if (WorkPhone != null && WorkPhone != "")
            {
                finalNumberString += "W: " + WorkPhone + "\r\n";
            }

            return finalNumberString;
        }

        
        private string DetailEmails()
        {
            string finalEmailString = "\r\n";

            if (FirstEmail != null && FirstEmail != "")
            {
                finalEmailString += FirstEmail + "\r\n";
            }

            if (SecondEmail != null && SecondEmail != "")
            {
                finalEmailString += SecondEmail + "\r\n";
            }

            if (ThirdEmail != null && ThirdEmail != "")
            {
                finalEmailString += ThirdEmail + "\r\n";
            }

            return finalEmailString;
        }

        private string DetailPerson()
        {
            string finalPersonString = "";

            if (AllNames != null && AllNames != "")
            {
                finalPersonString += AllNames + "\r\n";
            }

            if (Address != null && Address != "")
            {
                finalPersonString += Address + "\r\n";
            }

            return finalPersonString;
        }

        public static List<ContactData> GetAll()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from g in db.Contacts select g).ToList();
            }
        }
    }
}

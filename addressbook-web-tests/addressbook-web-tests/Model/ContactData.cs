using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

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

        public string FirstEmail{ get; set; }

        public string SecondEmail { get; set; }

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

        public string Id { get; set; }

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
    }
}

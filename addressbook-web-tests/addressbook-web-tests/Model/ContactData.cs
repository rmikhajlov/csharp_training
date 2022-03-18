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

        public string AllInfo
        {
            get
            {
                if (allInfo != null)
                {
                    return allInfo;
                }
                else if (String.IsNullOrEmpty(FirstName) && String.IsNullOrEmpty(LastName))
                {
                    return "";
                }
                else 
                {
                    return FirstName + " " + LastName + Address + DetailPhoneNumbers() + AllEmails;
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
            string finalNumberString = "";

            if (HomePhone != null && HomePhone != "")
            {
                finalNumberString += "H: " + HomePhone;
            }

            if (MobilePhone != null && MobilePhone != "")
            {
                finalNumberString += "M: " + MobilePhone;
            }

            if (WorkPhone != null && WorkPhone != "")
            {
                finalNumberString += "W: " + WorkPhone;
            }

            return finalNumberString;
        }
    }
}

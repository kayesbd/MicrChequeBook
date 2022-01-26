using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using KBZ.Utils.Infrastructure.Contract;

namespace KBZ.Utils.Infrastructure
{
    [Serializable]
    public abstract class BaseDomainModel<T> : NonAuditableDomainModel<T>, IDomainModel, IDataModel, IValidatable
        where T : BaseDomainModel<T>
    {
        public BaseDomainModel(bool isActive = true)
        {
            IsActive = isActive;
            this.InitializeModel();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (System.Reflection.PropertyInfo property in this.GetType().GetProperties())
            {

                if (property.GetIndexParameters().Length > 0)
                {
                    sb.Append("Indexed Property cannot be used");
                }
                else
                {
                    object val = property.GetValue(this, null);
                    if (val != null)
                    {
                        if (!string.IsNullOrEmpty(val.ToString()))
                        {
                            if (!property.Name.Contains("Password") && !property.Name.Contains("PIN"))
                            {
                                if ((property.Name.Contains("AccountToken"))
                                    || property.Name.Contains("DebitAccountNumber")
                                    || (property.Name.Equals("BankAccountNumber")))
                                {
                                    //  BankAccountNumber_KeyVer
                                    sb.Append(property.Name);
                                    sb.Append(": ");
                                    var accountNumber = property.GetValue(this, null);
                                    if (!string.IsNullOrWhiteSpace(accountNumber.ToString()))
                                    {
                                        if (accountNumber.ToString().Length > 8)
                                        {
                                            //var cardNumber = "3456123434561234";

                                            var firstDigits = accountNumber.ToString().Substring(0, 6);
                                            var lastDigits = accountNumber.ToString().Substring(accountNumber.ToString().Length - 4, 4);

                                            var requiredMask = new String('*', accountNumber.ToString().Length - firstDigits.Length - lastDigits.Length);

                                            var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);

                                            //sb.Append(property.GetValue(this, null));
                                            sb.Append(maskedString.ToString());
                                            sb.Append(",");
                                        }
                                    }


                                    //sb.Append(property.GetValue(this, null));

                                    //sb.Append(",");
                                }
                                else
                                {
                                    sb.Append(property.Name);
                                    sb.Append(": ");
                                    sb.Append(property.GetValue(this, null));
                                    sb.Append(",");
                                }

                            }
                        }
                    }


                }
            }
            return sb.ToString();

        }
        public void SetCreateProperties()
        {
            CustomUserPrincipal principal = Thread.CurrentPrincipal as CustomUserPrincipal;
            IsDeleted = false;
            CreatedDate = DateTime.UtcNow;
            //  DateModified = DateTime.UtcNow;
            if (principal != null)
            {
                CreatedBy = principal.PersonId;
                OrganizationId = principal.OrganizationId;
                // ModifiedBy = principal.PersonId;
                CreatedByName = principal.FullName;
                // ModifiedByName = principal.FullName;
            }
        }

        private void InitializeModel()
        {
            CustomUserPrincipal principal = Thread.CurrentPrincipal as CustomUserPrincipal;
            
            IsDeleted = false;
            DateCreated = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;


       
            //  DateModified = DateTime.UtcNow;
            if (principal != null)
            {
                CreatedBy = principal.PersonId;
                OrganizationId = principal.OrganizationId;
                  ModifiedBy = principal.PersonId;
                CreatedByName = principal.FullName;
                  ModifiedByName = principal.FullName;
            }
        }

        /// <summary>
        /// Validates the entity according to it's currently set Validator(s)
        /// Returns true / false if the object is valid and sets the brokenRules with what may be wrong with the entity
        /// </summary>
        /// <param name="brokenRules"></param>
        /// <returns></returns>
        public virtual bool Validate(out IEnumerable<string> brokenRules)
        {
            IValidator<T> validator = Validator.GetValidatorFor(this as T);
            // If we don't find any validators then we can't check it's valid!
            if (validator == null)
            {
                Console.WriteLine("Entity : " + typeof(T) +
                                  " has no validators and is being validated. Consider setting some defaults.");
                brokenRules = new List<String>();
                return true;
            }

            var result = validator.IsValid(this as T);
            brokenRules = validator.BrokenRules(this as T);
            return result;
        }

        public void SetUpdateProperties(long userId)
        {
            DateModified = DateTime.UtcNow;
            ModifiedBy = userId;
        }

        public void SetCreateProperties(long userId)
        {
            DateCreated = DateTime.UtcNow;
            CreatedBy = userId;
        }

        public void MarkDeleted(int userId)
        {
            IsDeleted = true;
        }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public long CreatedBy { get; set; }
        public long OrganizationId { get; set; }

        public string ModifiedByName { get; set; }
        public string CreatedByName { get; set; }

        public long skip { get; set; }
        public long take { get; set; }
        public long pageSize { get; set; }
        public long page { get; set; }
        public filter[] filters { get; set; }

        public void MarkDeleted(object userId)
        {
            throw new NotImplementedException();
        }
    }

    public class filter
    {
        public string field { get; set; }
        public string Operator { get; set; }
        public string value { get; set; }
        public string logic { get; set; }
    }
}
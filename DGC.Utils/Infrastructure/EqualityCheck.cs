using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace KBZ.Utils.Infrastructure
{
    public class EqualityCheck
    {
        public List<DetailAuditHistory> CheckEquality(object FirstObject, object SecondObject, ArrayList ignoreList)
       {
        // Object Matching
            //if (FirstObject == null && SecondObject == null)
            //{
            //    return null;
            //}
            //if (FirstObject == null || SecondObject == null)
            //{
            //    return null;
            //}
            Type firstType = FirstObject.GetType();
            if(SecondObject != null)
            { 
                 Type secondType = SecondObject.GetType();
           
                if (secondType != null)
                {
                    if (SecondObject.GetType() != firstType)
                    {
                        return null;
                    }
                }
            }

            var i = 0;
            List<DetailAuditHistory> DetailAuditHistoryModel = new List<DetailAuditHistory>();
            foreach (PropertyInfo propertyInfo in firstType.GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    string objName = propertyInfo.Name;

                    
                    object CurrentValue = propertyInfo.GetValue(FirstObject, null);
                    object PreviousValue = null;
                    if (SecondObject != null)
                    {
                         PreviousValue = propertyInfo.GetValue(SecondObject, null);
                    }
                   
                   

                    if(!ignoreList.Contains(objName))
                    {
                    
                    //}
                    //if (objName != "Id" && objName != "AuditBy" && objName != "AuditDate" && objName != "ModifiedBy")
                    //{
                        if (!object.Equals(PreviousValue, CurrentValue))
                        {
                            i = i + 1;
                            DetailAuditHistory Details = new DetailAuditHistory();
                            Details.PropertyName = objName;
                            if (objName == "BankAccountNo")
                            {
                                PreviousValue = PreviousValue ==  null?"": Crypto.DecryptStringAES(PreviousValue.ToString(), "");
                                CurrentValue = CurrentValue == null? "":Crypto.DecryptStringAES(CurrentValue.ToString(), "");
                            }
                            Details.PreviousValue = PreviousValue == null? "":PreviousValue.ToString();
                            Details.CurrentValue = CurrentValue == null? "":CurrentValue.ToString();
                            if (Details.PreviousValue != Details.CurrentValue)
                            {
                                DetailAuditHistoryModel.Add(Details);
                            }
                            
                        }
                      
                    
                    }
                    
                }
            }

            return DetailAuditHistoryModel;
       }
    }

    public class DetailAuditHistory
    {
        public string PropertyName { get; set; }
        public string PreviousValue { get; set; }
        public string CurrentValue { get; set; }


    }

}

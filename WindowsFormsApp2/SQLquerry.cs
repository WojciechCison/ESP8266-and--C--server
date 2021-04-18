using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class SQLquerry
    {
        
        public static void insertvalues(int t,int h)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            value addvalue = new value();
            addvalue.Temp = t;
            addvalue.Hum = h;
            db.values.InsertOnSubmit(addvalue);
            db.SubmitChanges();
        }

        public static IQueryable<value> refreshview()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var result = from tmp in db.values

                         select tmp;
            return result;
        }
    }
}

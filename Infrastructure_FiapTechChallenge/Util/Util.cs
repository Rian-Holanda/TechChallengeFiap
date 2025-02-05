using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_FiapTechChallenge.Util
{
    public class Util
    {
        public static DataTable ToDataTable(IEnumerable<object> list)
        {
            DataTable dtRetorno = new DataTable();

            if (list.Any())
            {
                var properties = list.First().GetType().GetProperties();


                foreach (var prop in properties)
                {
                    var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                    dtRetorno.Columns.Add(prop.Name, propType);

                }

                foreach (var item in list)
                {
                    var values = properties.Select(prop =>
                    {
                        var propValue = prop.GetValue(item);
                        return propValue != null ? propValue : DBNull.Value;
                    }).ToArray();

                    lock (dtRetorno.Rows)
                    {
                        dtRetorno.Rows.Add(values);
                    }
                }

                //Parallel.ForEach(list, item =>
                //{                    
                //    var values = properties.Select(prop =>
                //    {
                //        var propValue = prop.GetValue(item);
                //        return propValue != null ? propValue : DBNull.Value;
                //    }).ToArray();

                //    lock (dtRetorno.Rows)
                //    {
                //        dtRetorno.Rows.Add(values);
                //    }
                //});
            }

            return dtRetorno;
        }


        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            try
            {

                foreach (DataColumn column in dr.Table.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {

                        if (column.DataType.Name == "DateTime" && pro.Name == column.ColumnName)
                            pro.SetValue(obj, (dr[column.ColumnName] == DBNull.Value) ? null : dr[column.ColumnName], null);
                        else if (column.DataType.Name == "Decimal" && pro.Name == column.ColumnName)
                            pro.SetValue(obj, (dr[column.ColumnName] == DBNull.Value) ? 0 : dr[column.ColumnName], null);
                        else if (pro.Name == column.ColumnName && column.DataType.Name == "String")
                            pro.SetValue(obj, (dr[column.ColumnName] == DBNull.Value) ? string.Empty : dr[column.ColumnName], null);
                        else if (pro.Name == column.ColumnName && column.DataType.Name == "Int32")
                            pro.SetValue(obj, (dr[column.ColumnName] == DBNull.Value) ? 0 : dr[column.ColumnName], null);
                        else if (pro.Name == column.ColumnName && pro.PropertyType.Name != column.DataType.Name)
                            pro.SetValue(obj, (dr[column.ColumnName] == DBNull.Value) ? 0 : Convert.ToInt32(dr[column.ColumnName]), null);
                        else if (pro.Name == column.ColumnName && pro.PropertyType.Name == column.DataType.Name)
                            pro.SetValue(obj, (dr[column.ColumnName] == DBNull.Value) ? null : (dr[column.ColumnName]), null);
                        else
                            continue;
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }

        }
    }
}

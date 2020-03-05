using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;


namespace Postgresql.Services
{
    public interface IServices<T> where T : class  //T TİPİ SADECE CLASS'DIR
    {
        EntityResult<T> Insert(T entities);
        EntityResult<T> Delete(int id);
        EntityResult<T> Update(T entities);
        EntityResult<T> getId(int id); //id yi bul getir gibisinden
        EntityResult<T> getList();
        EntityResult<T> UpdateSelected(string table,string colum,int value,int id);

    }
}

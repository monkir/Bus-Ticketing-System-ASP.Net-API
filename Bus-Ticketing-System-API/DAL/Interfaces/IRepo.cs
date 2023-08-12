using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepo<classType, idType, returnType>
    {
        List<classType> All();
        classType get(idType key);
        returnType create(classType obj);
        returnType update(classType obj);
        returnType delete(idType key);
    }
}

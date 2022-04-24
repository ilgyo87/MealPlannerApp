using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IAccountDao
    {
        Accounts GetAccount(string username);
        //IList<Accounts> GetAllAccountsList();
        //IList<Accounts> GetAllAccountsListButMe(string username);
        Accounts PostAccount(Accounts account);
        Accounts UpdateAccount(Accounts account);
    }
}

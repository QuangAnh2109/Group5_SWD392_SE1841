
using Group5_SWD392_SE1841.Models;
using Microsoft.EntityFrameworkCore;

namespace Group5_SWD392_SE1841.Repositories.Impl
{
    public class MasterRepo : IMasterRepo
    {
        private readonly Group5Swd392Se1841Context _context;
        public MasterRepo(Group5Swd392Se1841Context context)
        {
            _context = context;
        }
        public async Task<Dictionary<int, string>> GetMasterDictionaryByTypeNameAsync(string typeName)
        {
            return await _context.Masters
                .Where(m => m.TypeName == typeName && !m.DeleteFlg)
                .ToDictionaryAsync(m => m.TypeKey, m => m.TypeValue);
        }
    }
}

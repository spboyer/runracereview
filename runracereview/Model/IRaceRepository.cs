using System.Collections.Generic;
using System.Threading.Tasks;

namespace runracereview.Model
{
  public interface IRaceRepository
  {
    Task<IEnumerable<Race>> GetAllRaces();
    Task<Race> GetRace(string name);
    Task Create(Race game);
    Task<bool> Update(Race game);
    Task<bool> Delete(string name);
  }
}
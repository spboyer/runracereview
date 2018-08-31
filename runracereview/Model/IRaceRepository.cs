using System.Collections.Generic;
using System.Threading.Tasks;

namespace runracereview.Model
{
  public interface IRaceRepository
  {
    Task<IEnumerable<Race>> GetAllRaces();
    Task<Race> GetRace(string name);
    Task Create(Race race);
    Task<bool> Update(Race race);
    Task<bool> Delete(string name);
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using runracereview.Model;

namespace runracereview.Pages
{
  public class RacesModel : PageModel
  {

    private readonly IRaceRepository _repository;

    public IEnumerable<Race> Races { get; private set; }

    public RacesModel(IRaceRepository repository)
    {
      _repository = repository;
    }
    public async Task OnGetAsync()
    {
      Races = await _repository.GetAllRaces();
    }
  }
}

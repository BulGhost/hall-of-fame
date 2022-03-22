using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallOfFame.DataAccess.Models;

public class PersonModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<SkillModel> Skills { get; set; }
}
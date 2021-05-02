using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace Bowling.Api
{
    public class ScoreRequest : IValidatableObject
    {
        [Required]
        [MinLength(Rolls.Min, ErrorMessage = "Input should contain at least 1 number.")]
        [MaxLength(Rolls.Max, ErrorMessage = "Too many inputs.")]
        [JsonPropertyName("pinsDowned")]
        public List<int> PinsDowned { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var pindowned in PinsDowned)
            {
                if (pindowned < Bowling.Api.PinsDowned.Min || pindowned > Bowling.Api.PinsDowned.Max)
                {
                    yield return new ValidationResult("Invalid pindowned number.");
                }
            }

            if (PinsDowned.Sum(x => x) > Bowling.Api.PinsDowned.TotalMax)
            {
                yield return new ValidationResult("Too many inputs.");
            }
        }
    }

}
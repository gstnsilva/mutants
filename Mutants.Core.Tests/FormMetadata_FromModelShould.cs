using Mutants.Core;
using Mutants.Core.Forms;
using Mutants.Core.Infrastructure;
using Mutants.Models;
using Xunit;

namespace Mutants.Core.Tests
{
    public class FormMetadata_FromModelShould
    {
        [Fact]
        public void ReturnFormFieldsAndLink()
        {
            const string routeName = "SomeRoute";
            var form = FormMetadata.FromModel(
                    new DnaForm(),
                    Link.ToForm(routeName, relations: Form.CreateRelation));
            Assert.NotNull(form);
            Assert.NotNull(form.Self);
            Assert.Equal(routeName, form.Self.RouteName);
            Assert.Equal("POST", form.Self.Method);
            var relation = Assert.Single(form.Self.Relations);
            Assert.Equal(Form.CreateRelation, relation);
            var formField = Assert.Single(form.Value);
            Assert.Equal(nameof(DnaForm.Dna).ToLower(), formField.Name);
        }
    }
}
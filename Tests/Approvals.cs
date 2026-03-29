using PublicApiGenerator;
using Shouldly;

namespace Tests;

public class Approvals
{
    [Theory]
    [InlineData(typeof(Shane32.EnphaseAPI.EnphaseClient))]
    public void ApiTest(Type type)
    {
        var api = type.Assembly.GeneratePublicApi(
            new ApiGeneratorOptions {
                IncludeAssemblyAttributes = false,
                DenyNamespacePrefixes = []
            }) + Environment.NewLine;
        api.ShouldMatchApproved(o => o.NoDiff().WithDiscriminator(type.Assembly.GetName().Name!));
    }
}

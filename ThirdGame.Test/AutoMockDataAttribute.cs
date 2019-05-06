using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace ThirdGame.Tests
{
    public class AutoMockDataAttribute : AutoDataAttribute
    {
        public AutoMockDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture();
                fixture.Customize(
                    new AutoNSubstituteCustomization { ConfigureMembers = true }
                );
                return fixture;
            })
        {
        }
    }
}

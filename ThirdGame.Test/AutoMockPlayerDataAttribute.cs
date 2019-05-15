using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Common;
using NSubstitute;

namespace ThirdGame.Tests
{
    public class AutoMockPlayerDataAttribute : AutoDataAttribute
    {
        public AutoMockPlayerDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture();
                fixture.Customize(
                    new AutoNSubstituteCustomization { ConfigureMembers = true }
                );

                var inputs = Substitute.For<Inputs>();
                fixture.Register(() => new Player(
                        "Teste"
                        , inputs
                        , fixture.Create<Camera2d>()
                        , new NetworkHandler(Substitute.For<UdpService>(), inputs)
                        , true
                    )
                );
                return fixture;
            })
        {
        }
    }
}

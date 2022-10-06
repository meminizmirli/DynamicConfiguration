using System.Linq;
using MoqAssist.Core;

namespace DynamicConfiguration.UnitTests.Base
{
    public class CommandTestBase<T>
        where T : class
    {
        protected readonly T _handler;
        protected readonly MoqAssist<T> _handlerMoqAssist;

        protected CommandTestBase()
        {
            _handlerMoqAssist = MoqAssist<T>.Construct(new DefaultMockDictionary());
            _handler = _handlerMoqAssist.GetConstructors().FirstOrDefault();
        }
    }
}
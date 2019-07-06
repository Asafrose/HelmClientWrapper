using Humanizer;
using Infrastructure;
using System.Collections;
using System.Linq;

namespace HelmClientWrapper
{
    public abstract class HelmCommand<TOutput>
    {
        protected readonly string CommandName;

        protected HelmCommand(string commandName) =>
            CommandName = Ensure.NotNullOrWhitespace(nameof(commandName), commandName);

        public abstract override string ToString();

        protected string GetFlagsString() =>
            GetType().
                GetProperties().
                Where(_ => _.IsDefined(typeof(FlagAttribute), false)).
                Select(
                    _ =>
                    {
                        var value = _.GetValue(this);
                        if (_.PropertyType == typeof(bool))
                        {
                            return (bool) value
                                ? $"--{_.Name.Kebaberize()}"
                                : null;
                        }

                        if (_.PropertyType == typeof(IEnumerable) && _.PropertyType != typeof(string))
                        {
                            return ((IEnumerable) value).
                                Cast<object>().
                                Select(__ => $"--{_.Name.Singularize(false).Kebaberize()} {__}").
                                ToJoinedString(' ');
                        }

                        return value != null
                            ? $"--{_.Name.Kebaberize()} {value}"
                            : null;
                    }).
                WhereNotNull().
                ToJoinedString(' ');
    }
}
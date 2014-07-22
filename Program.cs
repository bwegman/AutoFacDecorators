using System;
using Autofac;

public interface IBolCommand<TInput, TOutput>
{
    TOutput Execute(TInput input);
}

public interface IMapper<TInput, TOutput>
{
    TOutput Map(TInput input);
}

public class Mapper<TInput, TOutput> : IMapper<TInput, TOutput>
    where TOutput : class
{
    public TOutput Map(TInput input)
    {
        return null;
    }
}

public class MappingAdapter<TOuterInput, TOuterOutput, TInnerInput, TInnerOutput> : IBolCommand<TOuterInput, TOuterOutput>
{
    private readonly IMapper<TOuterInput, TInnerInput> inputMapper;
    private readonly IMapper<TInnerOutput, TOuterOutput> outputMapper;
    private readonly IBolCommand<TInnerInput, TInnerOutput> innerCommand;

    public MappingAdapter(
        IMapper<TOuterInput, TInnerInput> inputMapper,
        IMapper<TInnerOutput, TOuterOutput> outputMapper,
        IBolCommand<TInnerInput, TInnerOutput> innerCommand
        )
    {
        this.inputMapper = inputMapper;
        this.outputMapper = outputMapper;
        this.innerCommand = innerCommand;
    }

    public TOuterOutput Execute(TOuterInput input)
    {
        var input27 = inputMapper.Map(input);

        var output27 = innerCommand.Execute(input27);

        return outputMapper.Map(output27);
    }
}


namespace Bol26
{
    public class aanvragenWO
    {
    }

    public class aanvragenWOAntw
    {
    }
}

namespace Bol27
{
    //public class BolCommandAanvragenWO : IBolCommand<aanvragenWO, aanvragenWOAntw>
    //{
    //    public aanvragenWOAntw Execute(aanvragenWO aanvragenWo)
    //    {
    //        Console.WriteLine("Blup27");

    //        return null;
    //    }
    //}

    public class aanvragenWO
    {
    }

    public class aanvragenWOAntw
    {
    }
}

namespace Bol28
{
    public class BolCommandAanvragenWO : IBolCommand<aanvragenWO, aanvragenWOAntw>
    {
        public aanvragenWOAntw Execute(aanvragenWO aanvragenWo)
        {
            Console.WriteLine("Blup28");

            return null;
        }
    }

    public class aanvragenWO
    {
    }

    public class aanvragenWOAntw
    {
    }

}

namespace Blup {

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new Autofac.ContainerBuilder();
            
            builder.RegisterGeneric(typeof(Mapper<,>))
                .As(typeof(IMapper<,>));

            // Module 26

            builder
                .RegisterType<MappingAdapter<Bol26.aanvragenWO, Bol26.aanvragenWOAntw, Bol27.aanvragenWO, Bol27.aanvragenWOAntw>>()
                .AsImplementedInterfaces();

            // Module 27

            builder
                .RegisterType<MappingAdapter<Bol27.aanvragenWO, Bol27.aanvragenWOAntw, Bol28.aanvragenWO, Bol28.aanvragenWOAntw>>()
                .AsImplementedInterfaces();


            //builder.RegisterGeneric(typeof(MappingAdapter<,,,>))
            //    .As(typeof(IBolCommand<,>));

            //builder.RegisterType<Bol27.BolCommandAanvragenWO>()
            //    .AsImplementedInterfaces();


            builder.RegisterType<Bol28.BolCommandAanvragenWO>()
                .AsImplementedInterfaces();

            var container = builder.Build();

            var command = container.Resolve<IBolCommand<Bol26.aanvragenWO, Bol26.aanvragenWOAntw>>();

            command.Execute(new Bol26.aanvragenWO());

            var command27 = container.Resolve<IBolCommand<Bol27.aanvragenWO, Bol27.aanvragenWOAntw>>();

            command27.Execute(new Bol27.aanvragenWO());

            var command28 = container.Resolve<IBolCommand<Bol28.aanvragenWO, Bol28.aanvragenWOAntw>>();

            command28.Execute(new Bol28.aanvragenWO());
        }
    }
}

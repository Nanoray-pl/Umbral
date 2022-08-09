using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Nanoray.Umbral
{
    public interface ICombinedProxyFactory
    {
        TCombined Proxy<TCombined, TComponent1, TComponent2>(
            Func<TComponent1> component1,
            Func<TComponent2> component2
        )
            where TCombined : TComponent1, TComponent2
            where TComponent1 : class
            where TComponent2 : class;

        TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3
        )
            where TCombined : TComponent1, TComponent2, TComponent3
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class;

        TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3,
            Func<TComponent4> component4
        )
            where TCombined : TComponent1, TComponent2, TComponent3, TComponent4
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class
            where TComponent4 : class;

        TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3,
            Func<TComponent4> component4,
            Func<TComponent5> component5
        )
            where TCombined : TComponent1, TComponent2, TComponent3, TComponent4, TComponent5
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class
            where TComponent4 : class
            where TComponent5 : class;

        TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3,
            Func<TComponent4> component4,
            Func<TComponent5> component5,
            Func<TComponent6> component6
        )
            where TCombined : TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class
            where TComponent4 : class
            where TComponent5 : class
            where TComponent6 : class;

        TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3,
            Func<TComponent4> component4,
            Func<TComponent5> component5,
            Func<TComponent6> component6,
            Func<TComponent7> component7
        )
            where TCombined : TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class
            where TComponent4 : class
            where TComponent5 : class
            where TComponent6 : class
            where TComponent7 : class;

        TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3,
            Func<TComponent4> component4,
            Func<TComponent5> component5,
            Func<TComponent6> component6,
            Func<TComponent7> component7,
            Func<TComponent8> component8
        )
            where TCombined : TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class
            where TComponent4 : class
            where TComponent5 : class
            where TComponent6 : class
            where TComponent7 : class
            where TComponent8 : class;

        TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3,
            Func<TComponent4> component4,
            Func<TComponent5> component5,
            Func<TComponent6> component6,
            Func<TComponent7> component7,
            Func<TComponent8> component8,
            Func<TComponent9> component9
        )
            where TCombined : TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class
            where TComponent4 : class
            where TComponent5 : class
            where TComponent6 : class
            where TComponent7 : class
            where TComponent8 : class
            where TComponent9 : class;
    }

    public class CombinedProxyFactory : ICombinedProxyFactory
    {
        private readonly ModuleBuilder ModuleBuilder;

        public CombinedProxyFactory(ModuleBuilder moduleBuilder)
        {
            this.ModuleBuilder = moduleBuilder;
        }

        public TCombined Proxy<TCombined, TComponent1, TComponent2>(
            Func<TComponent1> component1,
            Func<TComponent2> component2
        )
            where TCombined : TComponent1, TComponent2
            where TComponent1 : class
            where TComponent2 : class
            => PrivateProxy<TCombined, TComponent1, TComponent2, IUnit, IUnit, IUnit, IUnit, IUnit, IUnit, IUnit>(
                component1, component2, Unit.IProvider, Unit.IProvider, Unit.IProvider, Unit.IProvider, Unit.IProvider, Unit.IProvider, Unit.IProvider
            );

        public TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3
        )
            where TCombined : TComponent1, TComponent2, TComponent3
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class
            => PrivateProxy<TCombined, TComponent1, TComponent2, TComponent3, IUnit, IUnit, IUnit, IUnit, IUnit, IUnit>(
                component1, component2, component3, Unit.IProvider, Unit.IProvider, Unit.IProvider, Unit.IProvider, Unit.IProvider, Unit.IProvider
            );

        public TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3,
            Func<TComponent4> component4
        )
            where TCombined : TComponent1, TComponent2, TComponent3, TComponent4
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class
            where TComponent4 : class
            => PrivateProxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, IUnit, IUnit, IUnit, IUnit, IUnit>(
                component1, component2, component3, component4, Unit.IProvider, Unit.IProvider, Unit.IProvider, Unit.IProvider, Unit.IProvider
            );

        public TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3,
            Func<TComponent4> component4,
            Func<TComponent5> component5
        )
            where TCombined : TComponent1, TComponent2, TComponent3, TComponent4, TComponent5
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class
            where TComponent4 : class
            where TComponent5 : class
            => PrivateProxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, IUnit, IUnit, IUnit, IUnit>(
                component1, component2, component3, component4, component5, Unit.IProvider, Unit.IProvider, Unit.IProvider, Unit.IProvider
            );

        public TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3,
            Func<TComponent4> component4,
            Func<TComponent5> component5,
            Func<TComponent6> component6
        )
            where TCombined : TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class
            where TComponent4 : class
            where TComponent5 : class
            where TComponent6 : class
            => PrivateProxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, IUnit, IUnit, IUnit>(
                component1, component2, component3, component4, component5, component6, Unit.IProvider, Unit.IProvider, Unit.IProvider
            );

        public TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3,
            Func<TComponent4> component4,
            Func<TComponent5> component5,
            Func<TComponent6> component6,
            Func<TComponent7> component7
        )
            where TCombined : TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class
            where TComponent4 : class
            where TComponent5 : class
            where TComponent6 : class
            where TComponent7 : class
            => PrivateProxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, IUnit, IUnit>(
                component1, component2, component3, component4, component5, component6, component7, Unit.IProvider, Unit.IProvider
            );

        public TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3,
            Func<TComponent4> component4,
            Func<TComponent5> component5,
            Func<TComponent6> component6,
            Func<TComponent7> component7,
            Func<TComponent8> component8
        )
            where TCombined : TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class
            where TComponent4 : class
            where TComponent5 : class
            where TComponent6 : class
            where TComponent7 : class
            where TComponent8 : class
            => PrivateProxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, IUnit>(
                component1, component2, component3, component4, component5, component6, component7, component8, Unit.IProvider
            );

        public TCombined Proxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3,
            Func<TComponent4> component4,
            Func<TComponent5> component5,
            Func<TComponent6> component6,
            Func<TComponent7> component7,
            Func<TComponent8> component8,
            Func<TComponent9> component9
        )
            where TCombined : TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9
            where TComponent1 : class
            where TComponent2 : class
            where TComponent3 : class
            where TComponent4 : class
            where TComponent5 : class
            where TComponent6 : class
            where TComponent7 : class
            where TComponent8 : class
            where TComponent9 : class
            => PrivateProxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9>(
                component1, component2, component3, component4, component5, component6, component7, component8, component9
            );

        private TCombined PrivateProxy<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9>(
            Func<TComponent1> component1,
            Func<TComponent2> component2,
            Func<TComponent3> component3,
            Func<TComponent4> component4,
            Func<TComponent5> component5,
            Func<TComponent6> component6,
            Func<TComponent7> component7,
            Func<TComponent8> component8,
            Func<TComponent9> component9
        )
        {
            throw new NotImplementedException();
        }

        private (Type, ConstructorInfo) CreateProxyType<TCombined, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9>()
        {
            // define proxy type
            TypeBuilder proxyType = ModuleBuilder.DefineType($"{Guid.NewGuid()}", TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Sealed);
            Type rawFuncType = typeof(Func<>);
            Type[] targetInterfaceTypes = new[] { typeof(TComponent1), typeof(TComponent2), typeof(TComponent3), typeof(TComponent4), typeof(TComponent5), typeof(TComponent6), typeof(TComponent7), typeof(TComponent8), typeof(TComponent9) };
            Type[] targetInterfaceProviderTypes = targetInterfaceTypes.Select(interfaceType => rawFuncType.MakeGenericType(interfaceType)).ToArray();

            if (!typeof(TCombined).IsInterface)
                throw new ArgumentException($"Type {typeof(TCombined)} is not an interface.");
            foreach (var targetInterfaceType in targetInterfaceTypes)
                if (!targetInterfaceType.IsInterface)
                    throw new ArgumentException($"Type {targetInterfaceType} is not an interface.");

            proxyType.AddInterfaceImplementation(typeof(TCombined));

            FieldBuilder[] targetFields = targetInterfaceProviderTypes.Select((interfaceProviderType, index) => proxyType.DefineField($"Component{index + 1}", interfaceProviderType, FieldAttributes.Private | FieldAttributes.InitOnly)).ToArray();

            // create constructor which accepts target instances, and sets fields
            ConstructorBuilder constructor = proxyType.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard | CallingConventions.HasThis, targetInterfaceProviderTypes);
            {
                ILGenerator il = constructor.GetILGenerator();

                // call base constructor
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Call, typeof(object).GetConstructor(Array.Empty<Type>())!);

                // set target instance fields
                for (int i = 0; i < targetInterfaceProviderTypes.Length; i++)
                {
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldarg, i + 1);
                    il.Emit(OpCodes.Stfld, targetFields[i]);
                }

                il.Emit(OpCodes.Ret);
            }

            foreach (var interfaceType in FindInterfaces(typeof(TCombined)))
            {
                var interfaceTypeMethods = interfaceType.GetMethods();
                if (interfaceTypeMethods.Length == 0)
                    continue;

                for (int i = 0; i < targetInterfaceTypes.Length; i++)
                {
                    Type targetInterfaceType = targetInterfaceTypes[i];
                    if (!interfaceType.IsAssignableTo(targetInterfaceType))
                        continue;

                    foreach (var interfaceTypeMethod in interfaceTypeMethods)
                    {
                        ParameterInfo[] parameters = interfaceTypeMethod.GetParameters();
                        MethodBuilder proxyMethod = proxyType.DefineMethod(
                            $"{interfaceType.FullName ?? interfaceType.Name}.{interfaceTypeMethod.Name}",
                            MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual,
                            CallingConventions.HasThis
                        );

                        Type[] genericParameters = proxyMethod.GetGenericArguments();
                        GenericTypeParameterBuilder[] genericParameterBuilders = genericParameters.Length == 0 ? Array.Empty<GenericTypeParameterBuilder>() : proxyMethod.DefineGenericParameters(genericParameters.Select(p => p.Name).ToArray());
                        for (int j = 0; j < genericParameterBuilders.Length; j++)
                        {
                            genericParameterBuilders[j].SetGenericParameterAttributes(genericParameters[j].GenericParameterAttributes);
                            Type[] constraints = genericParameters[j].GetGenericParameterConstraints();
                            Type? baseConstraint = constraints.Where(t => !t.IsInterface).FirstOrDefault();
                            Type[] interfaceConstraints = constraints.Where(t => t.IsInterface).ToArray();
                            if (baseConstraint is not null)
                                genericParameterBuilders[j].SetBaseTypeConstraint(baseConstraint);
                            if (interfaceConstraints.Length != 0)
                                genericParameterBuilders[j].SetInterfaceConstraints(interfaceConstraints);
                        }

                        Type[] argTypes = interfaceTypeMethod.GetParameters()
                            .Select(a => a.ParameterType)
                            .Select(t => t.IsGenericMethodParameter ? genericParameterBuilders[t.GenericParameterPosition] : t)
                            .ToArray();
                        Type returnType = interfaceTypeMethod.ReturnType.IsGenericMethodParameter ? genericParameterBuilders[interfaceTypeMethod.ReturnType.GenericParameterPosition] : interfaceTypeMethod.ReturnType;

                        proxyMethod.SetSignature(
                            returnType: returnType,
                            returnTypeRequiredCustomModifiers: interfaceTypeMethod.ReturnParameter.GetRequiredCustomModifiers(),
                            returnTypeOptionalCustomModifiers: interfaceTypeMethod.ReturnParameter.GetOptionalCustomModifiers(),
                            parameterTypes: argTypes,
                            parameterTypeRequiredCustomModifiers: parameters.Select(p => p.GetRequiredCustomModifiers()).ToArray(),
                            parameterTypeOptionalCustomModifiers: parameters.Select(p => p.GetOptionalCustomModifiers()).ToArray()
                        );

                        for (int j = 0; j < argTypes.Length; j++)
                            proxyMethod.DefineParameter(j, parameters[j].Attributes, parameters[j].Name);

                        ILGenerator il = proxyMethod.GetILGenerator();
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Ldfld, targetFields[i]);
                        for (int j = 0; j < argTypes.Length; j++)
                            il.Emit(OpCodes.Ldarg, j + 1);
                        il.Emit(OpCodes.Call, targetFields[i].FieldType.GetMethod("Invoke")!);
                        il.Emit(OpCodes.Ret);

                        proxyType.DefineMethodOverride(proxyMethod, interfaceTypeMethod);
                    }

                    goto interfaceTypeLoopContinue;
                }

                throw new InvalidOperationException($"Type {typeof(TCombined)} implements an interface {interfaceType} which isn't implemented by any of the component types.");
            interfaceTypeLoopContinue:;
            }

            return (proxyType, constructor);
        }

        private static IEnumerable<Type> FindInterfaces(Type baseType)
        {
            if (baseType.IsInterface)
                yield return baseType;
            foreach (var interfaceType in baseType.GetInterfaces())
                foreach (var interfaceType2 in FindInterfaces(interfaceType))
                    yield return interfaceType2;
        }
    }
}

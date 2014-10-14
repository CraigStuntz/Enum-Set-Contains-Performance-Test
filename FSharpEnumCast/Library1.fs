namespace FSharpEnumCast
open System
open System.Linq

type EnumDenseIntSet<'TEnum when 'TEnum: enum<int>
                         and 'TEnum: equality>
                         (values: seq<'TEnum>) = 
    let allEnumValues = System.Enum.GetValues(typeof<'TEnum>).Cast<'TEnum>().ToArray()
    let items = 
        let length = System.Enum.GetValues(typeof<'TEnum>).Cast<'TEnum>().Count()
        Array.init(length)
            (fun i -> Seq.exists ((=) (LanguagePrimitives.EnumOfValue i)) values)
    member this.Contains(value: 'TEnum) = items.[LanguagePrimitives.EnumToValue value]

type ConvertibleDenseIntSet<'TEnum when 'TEnum: enum<int>
                                    and 'TEnum: equality
                                    and 'TEnum:> IConvertible>
                                    (values: seq<'TEnum>) = 
    let allEnumValues = System.Enum.GetValues(typeof<'TEnum>).Cast<'TEnum>().ToArray()
    let items = 
        let length = System.Enum.GetValues(typeof<'TEnum>).Cast<'TEnum>().Count()
        Array.init(length)
            (fun i -> Seq.exists ((=) (LanguagePrimitives.EnumOfValue i)) values)
    member this.Contains(value: 'TEnum) = items.[value.ToInt32(System.Globalization.CultureInfo.InvariantCulture)]

type UglyDenseIntSet<'TEnum when 'TEnum: enum<int>
                                    and 'TEnum: equality>
                                    (values: seq<'TEnum>) = 
    let allEnumValues = System.Enum.GetValues(typeof<'TEnum>).Cast<'TEnum>().ToArray()
    let items = 
        let length = System.Enum.GetValues(typeof<'TEnum>).Cast<'TEnum>().Count()
        Array.init(length)
            (fun i -> Seq.exists ((=) (LanguagePrimitives.EnumOfValue i)) values)
    member this.Contains(value: int) = items.[value]

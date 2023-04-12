using AutoMapper;

namespace Bank.Application.Common.Mapping;

public interface IMapWith<T>
{
    //данный метод, реализованный по умолчанию, будет просто создавать конфигурацию из исходного
    //типа Т в тип назначения
    void Mapping(Profile profile) =>
        profile.CreateMap(typeof(T), GetType());
}

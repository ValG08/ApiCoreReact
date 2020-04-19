using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TestWebApiCore.Common.Models;
using TestWebApiCore.DAL.Entities;

namespace TestWebApiCore.BLL.AutomapperProfiles
{
    public class EntityDtoProfile : Profile
    {
        public EntityDtoProfile()
        {
            CreateMap<NoteModel, Note>()
                   .ForMember(
                       dest => dest.NoteMessage,
                       opt => opt.MapFrom(src => src.NoteMessage));

            CreateMap<Note, NoteModel>()
                    .ForMember(
                         dest => dest.NoteMessage,
                         opt => opt.MapFrom(src => src.NoteMessage));
        }
    }
}

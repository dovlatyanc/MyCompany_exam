﻿using MyCompany.Domain.Entities;
using MyCompany.Models;

namespace MyCompany.Infrastructure
{
    public static class HelperDTO
    {
        //Service=>ServiceDTO
        public static ServiceDTO TransformService(Service entity)
        {
            ServiceDTO entityDTO = new ServiceDTO();   
            entityDTO.Id= entity.Id;
            entityDTO.CategoryName=entity.ServiceCategory?.Title;
            entityDTO.Title = entity.Title;
            entityDTO.Description = entity.Description;
            entityDTO.DescriptionShort= entity.DescriptionShort;
            entityDTO.PhotoFileName = entity.Photo;
            entityDTO.Type = entity.Type.ToString();

            return entityDTO;

        }

        public static IEnumerable<ServiceDTO> TransformServices(IEnumerable<Service> entities) { 
            List<ServiceDTO> entitiesDTO = new List<ServiceDTO>();

            foreach (Service entity in entities) { 

                entitiesDTO.Add(TransformService(entity));
            }

            return entitiesDTO; 
        }
    }
}

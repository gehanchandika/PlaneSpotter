using PlaneSpotter.Models;
using PlaneSpotter.Repo.Entities;
using PlaneSpotter.Repo.Repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlaneSpotter.Services
{
    public class PlaneSpotterService : IPlaneSpotterService
    {
        private readonly IUnitofWork unitOfWork;


        public PlaneSpotterService(IUnitofWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public IEnumerable<AircraftModel> GetAllSpottedAircraftInfo()
        {
            var list = unitOfWork.AircraftRepository<AircraftEntity>().GetAll().ToList();

            if (list != null && list.Count > 0)
            {
                return list.Select(x => new AircraftModel
                {
                    AircraftId = x.AircraftId,
                    AircraftMake = x.AircraftMake,
                    AircraftRegistration = x.AircraftRegistration,
                    AircrafModel = x.AircraftModel
                });
            }
            return null;
        }

        public IEnumerable<SpottingModel> GetAllSpottingInfo()
        {
            var spotList = unitOfWork.PlaneSpotterRepository<SpotEntity>().GetAll().ToList();
            var aircraftList = unitOfWork.AircraftRepository<AircraftEntity>().GetAll().ToList();

            var aircraftSpotList = spotList.Join(aircraftList, x => x.AircraftId, y => y.AircraftId, (i, j) => new SpottingModel
            {
                SpotId = i.SpotId,
                Location = i.Location,
                SpottedDateTime = i.SpottedDateTime,
                AircraftId = j.AircraftId,
                AircraftMake = j.AircraftMake,
                AircraftRegistration = j.AircraftRegistration,
                AircraftModel = j.AircraftModel,
                SpottedImageFile = i.SpottedImageFile
            }).ToList();

            if (aircraftSpotList != null && aircraftSpotList.Count > 0)
            {
                return aircraftSpotList;
            }
            return null;
        }

        public void SavePlaneSpotInfo(SpottingModel model)
        {
            long tempAircraftId;

            var newAircraftEntity = unitOfWork.AircraftRepository<AircraftEntity>().GetByRegistration(model.AircraftRegistration);

            if (newAircraftEntity == null)
                tempAircraftId = SaveAircraftInfo(model);
            else
                tempAircraftId = newAircraftEntity.AircraftId;

            unitOfWork.PlaneSpotterRepository<SpotEntity>().Add(new SpotEntity
            {
                AircraftId = tempAircraftId,
                IsDeleted = false,
                Location = model.Location,
                SpottedDateTime = model.SpottedDateTime,
                SpottedImageFile = model.SpottedImageFile
            });
            unitOfWork.SaveChanges();

        }

        public void UpdatePlaneSpotInfo(SpottingModel model)
        {
            long tempAircraftId;
            var selectedEntity = unitOfWork.PlaneSpotterRepository<SpotEntity>().Get(model.SpotId);

            if (selectedEntity != null)
            {
                var newAircraftEntity = unitOfWork.AircraftRepository<AircraftEntity>().GetByRegistration(model.AircraftRegistration);

                if (newAircraftEntity == null)
                    tempAircraftId = SaveAircraftInfo(model);
                else
                    tempAircraftId = newAircraftEntity.AircraftId;

                selectedEntity.AircraftId = tempAircraftId;
                selectedEntity.Location = model.Location;
                selectedEntity.SpottedDateTime = model.SpottedDateTime;
                selectedEntity.SpottedImageFile = model.SpottedImageFile;


                unitOfWork.PlaneSpotterRepository<SpotEntity>().Update(selectedEntity);
                unitOfWork.SaveChanges();
            }
        }

        public void DeleteSelectedSpottedPlanes(long id)
        {
            unitOfWork.PlaneSpotterRepository<SpotEntity>().Delete(id);

            unitOfWork.SaveChanges();
        }

        public long SaveAircraftInfo(SpottingModel model)
        {
            var obj = unitOfWork.AircraftRepository<AircraftEntity>().Add(new AircraftEntity
            {
                AircraftRegistration = model.AircraftRegistration,
                AircraftMake = model.AircraftMake,
                AircraftModel = model.AircraftModel
            });

            unitOfWork.SaveChanges();

            return obj.AircraftId;
        }
    }
}

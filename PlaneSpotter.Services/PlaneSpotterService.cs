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

        public IEnumerable<SpotInfoModel> GetAllSpottingInfo()
        {
            var spotList = unitOfWork.PlaneSpotterRepository<SpotEntity>().GetAll().ToList();
            var aircraftList = unitOfWork.AircraftRepository<AircraftEntity>().GetAll().ToList();

            var aircraftSpotList = spotList.Join(aircraftList, x => x.AircraftId, y => y.AircraftId, (i, j) => new SpotInfoModel
            {
                SpotId = i.SpotId,
                Location = i.Location,
                SpottedDateTime = i.SpottedDateTime,
                AircraftId = j.AircraftId,
                AircraftMake = j.AircraftMake,
                AircraftRegistration = j.AircraftRegistration,
                AircraftModel = j.AircraftModel
            }).ToList();

            if (aircraftSpotList != null && aircraftSpotList.Count > 0)
            {
                return aircraftSpotList;
            }
            return null;
        }

        public void SavePlaneSpotInfo(SpotInfoRichModel model)
        {
            if (model != null)
            {
                long tempAircraftId;

                var newAircraftEntity = unitOfWork.AircraftRepository<AircraftEntity>().GetByRegistration(model.AircraftRegistration);

                if (newAircraftEntity == null)
                    tempAircraftId = SaveAircraftInfo(model);
                else
                    tempAircraftId = newAircraftEntity.AircraftId;


                var obj = unitOfWork.PlaneSpotterRepository<SpotEntity>().Add(new SpotEntity
                {
                    AircraftId = tempAircraftId,
                    IsDeleted = false,
                    Location = model.Location,
                    SpottedDateTime = model.SpottedDateTime
                });

                unitOfWork.SaveChanges();

                if (model.SpotAircraftImage != "")
                    SaveSpottedAircraftImage(new SpotInfoImageModel { SpotId = obj.SpotId, SpotAircraftImage = model.SpotAircraftImage });
            }
        }

        public void UpdatePlaneSpotInfo(SpotInfoRichModel model)
        {
            if (model != null)
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

                    unitOfWork.PlaneSpotterRepository<SpotEntity>().Update(selectedEntity);
                    unitOfWork.SaveChanges();

                    if (model.SpotAircraftImage != "")
                        SaveSpottedAircraftImage(new SpotInfoImageModel { SpotId = model.SpotId, SpotAircraftImage = model.SpotAircraftImage });
                }
            }
        }

        public void DeleteSelectedSpottedPlanes(long id)
        {
            unitOfWork.PlaneSpotterRepository<SpotEntity>().Delete(id);

            unitOfWork.SaveChanges();
        }

        public long SaveAircraftInfo(SpotInfoRichModel model)
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

        public SpotInfoImageModel GetSpotInfoImage(long id)
        {
            var imageEntity = unitOfWork.ImageRepository<SpotImageEntity>().GetImageBySpotId(id);

            if (imageEntity != null)
                return new SpotInfoImageModel
                {
                    ImageId = imageEntity.ImageId,
                    SpotId = imageEntity.SpotId,
                    SpotAircraftImage = imageEntity.SpotAircraftImage
                };
            else
                return null;
        }

        public long SaveSpottedAircraftImage(SpotInfoImageModel model)
        {
            long imgId;
            var selectedEntity = unitOfWork.ImageRepository<SpotImageEntity>().GetImageBySpotId(model.SpotId);

            if (selectedEntity == null)
            {
                var obj = unitOfWork.ImageRepository<SpotImageEntity>().Add(new SpotImageEntity
                {
                    SpotId = model.SpotId,
                    SpotAircraftImage = model.SpotAircraftImage
                });

                unitOfWork.SaveChanges();

                imgId = obj.ImageId;
            }
            else
            {
                selectedEntity.SpotAircraftImage = model.SpotAircraftImage;

                unitOfWork.ImageRepository<SpotImageEntity>().Update(selectedEntity);

                unitOfWork.SaveChanges();

                imgId = selectedEntity.ImageId;
            }

            return imgId;
        }
    }
}

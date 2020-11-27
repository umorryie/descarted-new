using Descartes.Contex;
using Descartes.Controllers;
using Descartes.DifferenceDeterminator;
using Descartes.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Descartes.Repository
{
    public class DifferenceRepository: IDifferenceRepository
    {
        private IDifferenceDeterminatorResolver _differenceDeterminator;
        private DiffContext _diffContext;
        public DifferenceRepository(IDifferenceDeterminatorResolver differenceDeterminator, DiffContext diffContext)
        {
            _differenceDeterminator = differenceDeterminator;
            _diffContext = diffContext;
        }

        public string DetermineDifferences(int id)
        {
            DifferenceObject response = _diffContext.DifferenceObject.FirstOrDefault<DifferenceObject>(diffObject => diffObject.Id == id);

            return response != null ? response.DiffResult : null;
        }

        private void SaveRightPartOfEquation(RequestDifferenceInputHelper requestInput, int id)
        {
            try
            {
                DifferenceObject result = GetAllDatabaseContent().FirstOrDefault<DifferenceObject>(diffObject => diffObject.Id == id);

                if (result == null)
                {
                    DifferenceObject diffObject = new DifferenceObject() 
                    {
                        RightValue = requestInput.data,
                        Id = id
                    };

                    _diffContext.DifferenceObject.Add(diffObject);
                }
                else
                {
                    result.RightValue = requestInput.data;

                    if (result.LeftValue != null)
                    {
                        result.DiffResult = _differenceDeterminator.DetermineDifferences(result.LeftValue, result.RightValue);
                    }
                }

                _diffContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void SaveLeftpartOfEquation(RequestDifferenceInputHelper requestInput, int id)
        {
            try
            {
                DifferenceObject result = GetAllDatabaseContent().FirstOrDefault<DifferenceObject>(diffObject => diffObject.Id == id);

                if (result == null)
                {
                    DifferenceObject diffObject = new DifferenceObject()
                    {
                        LeftValue = requestInput.data,
                        Id = id
                    };

                    _diffContext.DifferenceObject.Add(diffObject);
                }
                else
                {
                    result.LeftValue = requestInput.data;

                    if (result.RightValue != null)
                    {
                        result.DiffResult = _differenceDeterminator.DetermineDifferences(result.LeftValue, result.RightValue);
                    }
                }

                _diffContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SaveObject(string identifier, RequestDifferenceInputHelper requestInput, int id)
        {
            if(identifier == "left")
            {
                SaveLeftpartOfEquation(requestInput, id);
            }
            else
            {
                SaveRightPartOfEquation(requestInput, id);
            }
        }

        public List<DifferenceObject> GetAllDatabaseContent()
        {
            return _diffContext.DifferenceObject.ToList<DifferenceObject>();
        }
    }
}

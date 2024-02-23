using Microsoft.AspNetCore.Mvc;
using MyFinances.WebApi.Models;
using MyFinances.WebApi.Models.Converters;
using MyFinances.WebApi.Models.Domains;
using MyFinances.WebApi.Models.Dtos;
using MyFinances.WebApi.Models.Response;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace MyFinances.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public OperationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[HttpGet]
        //public IEnumerable<Operation> Get2()
        //{
        //    return _unitOfWork.Operation.Get();
        //}

        //[HttpGet]
        //public IActionResult Get3()
        //{
        //    if (Jakiś warunek) 
        //    {
        //        return BadRequest(); lub return NotFound();
        //    }

        //    return Ok(_unitOfWork.Operation.Get());
        //}

        [HttpGet]
        public DataResponse<IEnumerable<OperationDto>> Get()
        {
            var response = new DataResponse<IEnumerable<OperationDto>>();

            try
            {
                //throw new Exception("Błąd");
                response.Data = _unitOfWork.Operation.Get().ToDtos();
            }
            catch (Exception ex)
            {
                //logowanie do pliku...
                response.Errors.Add(new Error(ex.Source, ex.Message));
                throw;
            }

            return response;
        }

        /// <summary>
        /// Get operation by id
        /// </summary>
        /// <param name="id">Operation id</param>
        /// <returns>DataResponce - Operation DTO</returns>
        [HttpGet("{id}")]
        public DataResponse<OperationDto> Get(int id)
        {
            var response = new DataResponse<OperationDto>();

            try
            {
                response.Data = _unitOfWork.Operation.Get(id)?.ToDto();
            }
            catch (Exception ex)
            {
                //logowanie do pliku...
                response.Errors.Add(new Error(ex.Source, ex.Message));
                throw;
            }

            return response;
        }

        [HttpGet("{records}/{page}")]
        public DataResponse<IEnumerable<OperationDto>> Get(int records, int page)
        {
            var response = new DataResponse<IEnumerable<OperationDto>>();

            try
            {
                if(records <= 0)
                    throw new ArgumentException(nameof(records));
                if (page <= 0)
                    throw new ArgumentException(nameof(page));

                //throw new Exception("Błąd");
                response.Data = _unitOfWork.Operation.Get(records, page).ToDtos();
            }
            catch (Exception ex)
            {
                //logowanie do pliku...
                response.Errors.Add(new Error(ex.Source, ex.Message));
                throw;
            }

            return response;
        }

        [HttpPost]
        public DataResponse<int> Add(OperationDto operationDto)
        {
            var response = new DataResponse<int>();

            try
            {
                var operation = operationDto.ToDao();
                 _unitOfWork.Operation.Add(operation);
                _unitOfWork.Complete();
                response.Data = operation.Id;
            }
            catch (Exception ex)
            {
                //logowanie do pliku...
                response.Errors.Add(new Error(ex.Source, ex.Message));
                throw;
            }

            return response;
        }

        [HttpPut]
        public Response Upadate(OperationDto operation)
        {
            var response = new Response();

            try
            {
                _unitOfWork.Operation.Update(operation.ToDao());
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                //logowanie do pliku...
                response.Errors.Add(new Error(ex.Source, ex.Message));
                throw;
            }

            return response;
        }

        [HttpDelete("id")]
        public Response Delete(int id)
        {
            var response = new Response();

            try
            {
                _unitOfWork.Operation.Delete(id);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                //logowanie do pliku...
                response.Errors.Add(new Error(ex.Source, ex.Message));
                throw;
            }

            return response;
        }

    }
}

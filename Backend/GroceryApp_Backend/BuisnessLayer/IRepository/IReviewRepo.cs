using BuisnessLayer.Model;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.IRepository
{
    public interface IReviewRepo
    {
        Task AddReviewAsync(ReviewModel model);
        Task<List<ReviewEntity>> GetAllReviewsByProductIdAsync(int id);
    }
}

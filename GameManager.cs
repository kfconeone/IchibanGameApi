using IChibanGameServer.Models;
using IChibanGameServer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IChibanGameServer
{
    public class GameManager(IchibanGameContext context)
    {

        private readonly IchibanGameContext _context = context;

        public LotDrawResultModel DrawStandard(string boxId, UpdateLotModel model) {
            using var transaction = _context.Database.BeginTransaction();
            var nowTicks = (int)DateTime.Now.Ticks;
            
            try
            {

                // 更新LotBox
                var lotBox = _context.LotBoxes.FirstOrDefault(lb => lb.BoxId == boxId) ?? throw new ArgumentException("找不到籤筒");
                if (lotBox.LockEndTime > nowTicks && lotBox.LastUserId != model.UserId) throw new ArgumentException("籤筒鎖定中");

                // 更新Lot
                var lots = _context.Lots.Where(l => model.PositionIndexs.Contains(l.PositionIndex));
                var lotsCount = lots.Count();
                if (lotsCount == 0) { 
                    throw new ArgumentException("找不到籤");
                }


                foreach (var lot in lots)
                {
                    if (!string.IsNullOrEmpty(lot.OwnerId))                     
                        throw new ArgumentException("已被抽走");


                    lot.OwnerId = model.UserId;
                    lot.FinishedDateTime = nowTicks;

                    _context.Lots.Update(lot);
                }


                //開始鎖定籤筒
                long tempLockEndTime = lotBox.LockEndTime;
                tempLockEndTime += 30 * lotsCount;


                //鎖定結束時間和當前時前如果超過300秒, 則鎖定為300秒
                long lockDurationInSeconds = tempLockEndTime - nowTicks;
                if (lockDurationInSeconds > 300 * TimeSpan.TicksPerSecond)
                {
                    lockDurationInSeconds = 300 * TimeSpan.TicksPerSecond;
                }

                lotBox.LockEndTime = nowTicks + lockDurationInSeconds;
                _context.LotBoxes.Update(lotBox);



                // 嘗試保存更改
                _context.SaveChanges();

                // 提交事務
                transaction.Commit();

                return new LotDrawResultModel
                {
                    LotBoxId = boxId,
                    Type = GameType.Standard,
                    LockEndTime = lotBox.LockEndTime,
                    Lots =
                    [
                        .. lots.Select(l => new ResponseLotModel
                        {
                            LotId = l.LotId,
                            OwnerId = l.OwnerId ?? "",
                            Level = l.Level
                        }),
                    ]
                };
                
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // 如果發生並發衝突，回滾事務
                transaction.Rollback();

                // 處理並發衝突的邏輯（例如，返回一個錯誤響應）
                throw;
            }
            catch (Exception ex)
            {
                // 對於其他類型的異常，也回滾事務
                transaction.Rollback();

                // 返回一個錯誤響應
                throw;

            }
        }

        public LotDrawResultModel DrawArena(string boxId, UpdateLotModel model)
        {
            using var transaction = _context.Database.BeginTransaction();
            var nowTicks = (int)DateTime.Now.Ticks;

            try
            {

                // 更新LotBox
                var lotBox = _context.LotBoxes.FirstOrDefault(lb => lb.BoxId == boxId) ?? throw new ArgumentException("找不到籤筒");
                if (lotBox.LockEndTime > nowTicks && lotBox.LastUserId != model.UserId) throw new ArgumentException("籤筒鎖定中");

                // 更新Lot
                var lots = _context.Lots.Where(l => model.PositionIndexs.Contains(l.PositionIndex));
                var lotsCount = lots.Count();
                if (lotsCount == 0)
                {
                    throw new ArgumentException("找不到籤");
                }


                foreach (var lot in lots)
                {
                    if (!string.IsNullOrEmpty(lot.OwnerId))
                        throw new ArgumentException("已被抽走");


                    lot.OwnerId = model.UserId;
                    lot.FinishedDateTime = nowTicks;

                    _context.Lots.Update(lot);
                }


                // 嘗試保存更改
                _context.SaveChanges();

                // 提交事務
                transaction.Commit();

                return new LotDrawResultModel
                {
                    LotBoxId = boxId,
                    Type = GameType.Arena,
                    LockEndTime = -1,
                    Lots =
                    [
                        .. lots.Select(l => new ResponseLotModel
                        {
                            LotId = l.LotId,
                            OwnerId = l.OwnerId ?? "",
                            Level = l.Level
                        }),
                    ]
                };

            }
            catch (DbUpdateConcurrencyException ex)
            {
                // 如果發生並發衝突，回滾事務
                transaction.Rollback();

                // 處理並發衝突的邏輯（例如，返回一個錯誤響應）
                throw;
            }
            catch (Exception ex)
            {
                // 對於其他類型的異常，也回滾事務
                transaction.Rollback();

                // 返回一個錯誤響應
                throw;

            }

        }

        public LotDrawResultModel DrawCollection(string boxId, UpdateLotModel model)
        {
            using var transaction = _context.Database.BeginTransaction();
            var nowTicks = (int)DateTime.Now.Ticks;

            try
            {

                // 更新LotBox
                var lotBox = _context.LotBoxes.FirstOrDefault(lb => lb.BoxId == boxId) ?? throw new ArgumentException("找不到籤筒");
                if (lotBox.LockEndTime > nowTicks && lotBox.LastUserId != model.UserId) throw new ArgumentException("籤筒鎖定中");

                // 更新Lot
                var lots = _context.Lots.Where(l => model.PositionIndexs.Contains(l.PositionIndex));
                var lotsCount = lots.Count();
                if (lotsCount == 0)
                {
                    throw new ArgumentException("找不到籤");
                }


                foreach (var lot in lots)
                {
                    if (!string.IsNullOrEmpty(lot.OwnerId))
                        throw new ArgumentException("已被抽走");


                    lot.OwnerId = model.UserId;
                    lot.FinishedDateTime = nowTicks;

                    _context.Lots.Update(lot);
                }


                // 嘗試保存更改
                _context.SaveChanges();

                // 提交事務
                transaction.Commit();

                return new LotDrawResultModel
                {
                    LotBoxId = boxId,
                    Type = GameType.Collection,
                    LockEndTime = -1,
                    Lots =
                    [
                        .. lots.Select(l => new ResponseLotModel
                        {
                            LotId = l.LotId,
                            OwnerId = l.OwnerId ?? "",
                            Level = ""
                        }),
                    ]
                };

            }
            catch (DbUpdateConcurrencyException ex)
            {
                // 如果發生並發衝突，回滾事務
                transaction.Rollback();

                // 處理並發衝突的邏輯（例如，返回一個錯誤響應）
                throw;
            }
            catch (Exception ex)
            {
                // 對於其他類型的異常，也回滾事務
                transaction.Rollback();

                // 返回一個錯誤響應
                throw;

            }

        }
    }
}

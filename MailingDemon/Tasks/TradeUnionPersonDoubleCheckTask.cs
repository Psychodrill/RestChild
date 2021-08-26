using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     Проверка дублей детей в других профсоюзных списках в рамках года
    /// </summary>
    [Task]
    public class TradeUnionPersonDoubleCheckTask : BaseTask
    {

        /// <summary>
        ///     Выполнить проверку
        /// </summary>
        protected override void Execute()
        {
            Logger.Info("TradeUnionPersonDoubleCheckTask started");

            using (var unitOfWork = new UnitOfWork())
            {
                

                var exec = unitOfWork.GetSet<TradeUnionPersonCheck>().Where(ss =>
                        !ss.NotActual &&
                        !ss.IsProcessed &&
                        ss.PersonCheckType == (long) TradeUnionPersonCheckTypeEnum.ApplicantDouble).OrderBy(ss => ss.Id)
                    .Take(500);


                foreach (var tupc in exec.ToList())
                {
                    var person = tupc.Person;
                    var tradeUnion = unitOfWork.GetSet<TradeUnionCamper>().Where(ss => ss.ChildId == tupc.PersonId).Select(ss => ss.TradeUnion).FirstOrDefault();
                    if (tradeUnion != null)
                    {
                        var personDoubls = unitOfWork.GetSet<TradeUnionList>().Where(ss => ss.Id != tradeUnion.Id &&
                                                                                              ss.YearOfRestId ==
                                                                                              tradeUnion.YearOfRestId &&
                                                                                              (ss.StateId == StateMachineStateEnum.TradeUnion.Finish ||
                                                                                               ss.StateId == StateMachineStateEnum.TradeUnion.OnAproving ||
                                                                                               ss.StateId == StateMachineStateEnum.TradeUnion.Approved ||
                                                                                               ss.StateId == StateMachineStateEnum.TradeUnion.Edit))
                            .SelectMany(ss => ss.Campers.Select(c => c.Child)).Where(c => c.Snils == person.Snils || (
                                                                                          c.DocumentNumber == person.DocumentNumber &&
                                                                                          c.DocumentSeria == person.DocumentSeria)).ToList();
                        tupc.PersonCheckResults?.AddRange(personDoubls);

                        unitOfWork.SaveChanges();
                    }

                    tupc.IsProcessed = true;
                    unitOfWork.SaveChanges();
                }
            }

            Logger.Info("TradeUnionPersonDoubleCheckTask finished");
        }

    }
}

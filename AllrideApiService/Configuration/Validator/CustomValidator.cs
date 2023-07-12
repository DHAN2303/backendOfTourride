using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiService.Configuration.Validator
{
    public class CustomValidator<T>
    {
        private readonly T _entity;
        private readonly Func<T, string> _propertyAccessorFunc;
        private readonly Func<IEnumerable<T>> _collectionAccessorFunc;

        public CustomValidator(T entity, Func<T, string> propertyAccessorFunc, Func<IEnumerable<T>> collectionAccessorFunc)
        {
            _entity = entity;
            _propertyAccessorFunc = propertyAccessorFunc;
            _collectionAccessorFunc = collectionAccessorFunc;
        }

        public bool Validate()
        {
            //Get all the entities by executing the lambda
            var entities = _collectionAccessorFunc();

            //Get the value of the entity that we are validating by executing the lambda
            var propertyValue = _propertyAccessorFunc(_entity);

            //Find the matching entity by executing the propertyAccessorFunc against the 
            //entities in the collection and comparing that with the result of the entity 
            //that is being validated. Warning SingleOrDefault will throw an exception if
            //multiple items match the supplied predicate
            //http://msdn.microsoft.com/en-us/library/vstudio/bb342451%28v=vs.100%29.aspx
            var matchingEntity = entities.SingleOrDefault(e => _propertyAccessorFunc(e) == propertyValue);
            return matchingEntity == null;
        }
    }
}
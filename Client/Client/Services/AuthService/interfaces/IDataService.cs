﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.WPF.Services.AuthService.interfaces;

public interface IDataService<T>
{
    Task<IEnumerable<T>> GetAll();

    Task<T> Get(int id);

    Task<T> Create(T entity);

    Task<T> Update(int id, T entity);

    Task<bool> Delete(int id);
}

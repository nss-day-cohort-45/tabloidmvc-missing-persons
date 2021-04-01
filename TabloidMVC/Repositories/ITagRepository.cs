﻿using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();

        Tag GetTagById(int id);

        void Add(Tag tag);

        void Delete(int tagId);

        void Edit(Tag tag);
    }
}
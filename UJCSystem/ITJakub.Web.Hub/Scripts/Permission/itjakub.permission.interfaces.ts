﻿

interface IPrintableItem {
    Name: string,
    Description: string,
}


interface IUser {
    Id: number;
    UserName: string;
    Email: string;
    FirstName: string;
    LastName: string;
}

interface IGroup {
    Id: number;
    Name: string;
    Description: string;
}

interface ICategory {
    Id: number;
    Description: string;
}

interface IBook {
    Id: number;
    Guid: string;
    Title: string;
}

interface ICategoryContent {
    Categories: ICategory[];
    Books: IBook[];
}
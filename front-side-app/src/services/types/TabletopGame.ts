export interface TabletopGameCreation {
  Title: string;
  PlayerCount: number;
  PLayingAge: number;
  PlayingTime: number;
  Description: string;
  Rules?: string;
  ThumbnailName: string;
  Images: Image[];
  Categories: Category[];
  BoardTypes: BoardType[];
  AditionalFiles: AditionalFile[];
  SaveAsDraft: boolean;
}

export interface Image {
  Location: string;
  Alias: string;
}

export interface Category {
  CategoryName: string;
}

export interface BoardType {
  BoardTypeName: string;
}

export interface AditionalFile {
  Name: string;
  Location: string;
}

export interface Invatation {
  Title: string;
  PlayersNeed: number;
  Map_X_Cords: number;
  Map_Y_Cords: number;
}

export interface Address {
  City: string;
  StreenName: string;
  Province: string;
  PostalCode: string;
  FullAddrss: string;
}

export interface SimpleTableTop {
  Id: string;
  Title: string;
}

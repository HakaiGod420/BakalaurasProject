export interface TabletopGameCreation {
  Title: string;
  PlayerCount: number | undefined;
  PLayingAge: number | undefined;
  PlayingTime: number | undefined;
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

export interface SimpleTableTop {
  Id: string;
  Title: string;
}

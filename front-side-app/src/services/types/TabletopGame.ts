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

export interface TableTopGameCard {
  GameBoardId: string;
  Title: string;
  ReleaseDate: string;
  ThumbnailURL: string | undefined;
  ThumbnailName: string;
}

export interface TableTopGameCardsResponse {
  BoardGames: TableTopGameCard[];
  TotalCount: number;
}

export interface SingleTabletopGame {
  BoardGameId: number;
  Title: string;
  PlayerCount: number | undefined;
  PlayableAge: number | undefined;
  Description: string;
  CreationTime: string;
  UpdateDate: string | undefined;
  Rules: string | undefined;
  Thumbnail_Location: string;
  CreatorName: string;
  CreatorId: number;
  Categories: string[];
  Types: string[];
  Images: TabletopImage[];
  Files: TabletopAditionalFile[];
}

export interface TabletopImage {
  FileName: string;
  Location: string;
}

export interface TabletopAditionalFile {
  FileName: string;
  Location: string;
}

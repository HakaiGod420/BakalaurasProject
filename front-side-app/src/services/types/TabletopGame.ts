import { Dayjs } from "dayjs";

export interface TabletopGameCreation {
  Title: string;
  PlayerCount: number | undefined;
  PlayableAge: number | undefined;
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
  PlayingTime: number | undefined;
  Rating: number | undefined;
  Description: string;
  CreationTime: string;
  UpdateDate: string | undefined;
  Rules: string | undefined;
  Thumbnail_Location: string;
  CreatorName: string;
  CreatorId: number;
  RatingCount: number;
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

export interface Filter {
  title: string;
  creationDate: Dayjs | null;
  rating: string;
  categories: CategoryOptions[];
  types: TypeOptions[];
}

export interface CategoryType {
  Value: string;
  Label: string;
}

export interface TypeOptions {
  value: string;
  label: string;
}

export interface CategoryOptions {
  value: string;
  label: string;
}

export interface TypesType {
  Value: string;
  Label: string;
}

export interface CategoriesAndTypes {
  Types: TypesType[];
  Categories: CategoryType[];
}

export interface TableTopGameItemForReview {
  GameBoardId: number;
  GameBoardName: string;
  GameBoardCreateDate: string;
  CreatorId: number;
  CreatorName: string;
}

export interface TableTopGamesForReviewResponse {
  GameBoardsForReview: TableTopGameItemForReview[];
  TotalCount: number;
}

export interface TabletopGameAproval {
  GameBoardId: number;
  IsAproved: boolean;
}

export interface TableTopGameForAdmin {
  GameBoardId: number;
  Title: string;
  GameBoardCreateDate: string;
  State: string;
  CreatorName: string;
  CreatorId: number;
  IsBlocked: boolean;
}

export interface TableTopGameListForAdminResponse {
  Boards: TableTopGameForAdmin[];
  TotalCount: number;
}

export interface TableTopGameIsBlocked {
  gameBoardId: number;
  isBlocked: boolean;
}

export interface UserCreatedGameBoardsReposne {
  GameBoards: UserCreatedGameBoard[];
  TotalCount: number;
}

export interface UserCreatedGameBoard {
  GameBoardId: number;
  Title: string;
  ImageUrl: string;
}

export interface EditGameBoardInfo {
  GameBoardId: number;
  Title: string;
  PlayerCount: number;
  PlayingTime: number | undefined;
  PlayableAge: number;
  Description: string;
  IsBlocked: boolean;
  Rules: string | undefined;
  SelectedTypes: TypesType[];
  SelectedCategories: CategoryType[];
}

export interface GetUpdageGameInfoResponse {
  EditGameBoardInfo: EditGameBoardInfo;
  AllTypes: TypesType[];
  AllCategories: CategoryType[];
}

export interface GalleryItem {
  ImageId: number;
  Location: string;
}

export interface GalleryForEdit {
  GalleryElements: GalleryItem[];
}

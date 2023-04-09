export interface Rating {
  BoardGameId: number | undefined;
  Rating: number;
  Comment: string | undefined;
}

export interface ReviewView {
  ReviewId: number;
  ProfileImage: string | undefined;
  Username: string;
  ReviewText: string | undefined;
  Rating: number;
  Written: string;
}

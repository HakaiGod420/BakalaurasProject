export interface MapCoordinates {
  Lat: number;
  Lng: number;
}

export interface JWTDeCode {
  UserId: string;
  Username: string;
  Email: string;
  Role: string;
  iat: number;
  exp: number;
  iss: string;
}

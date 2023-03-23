export interface Address {
  City: string;
  StreetName: string;
  Province: string;
  PostalCode: string | undefined;
  HouseNumber: number | undefined;
}

export interface Invitation {
  ActiveGameId: number;
  PlayersNeeded: number;
  MinimalAge: number;
  Map_X_Cords: number;
  Map_Y_Cords: number;
  Address: Address;
  InvitationDate: string;
}

export interface UserInvitation {
  ActiveGameId: number;
  BoardGameTitle: string;
  BoardGameId: number;
  EventDate: string;
  EventFullLocation: string;
  MaxPlayerCount: number;
  AcceptedCount: number;
  Map_X_Cords: number;
  Map_Y_Cords: number;
}

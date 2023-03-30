import L from "leaflet";
import "leaflet/dist/leaflet.css";
import React, { useState } from "react";
import toast from "react-hot-toast";
import { MapContainer, Marker, TileLayer, useMapEvents } from "react-leaflet";
import marker from "../../assets/images/market.png";
import { updateAddress } from "../../services/api/UserServics";
import { MapCoordinates } from "../../services/types/Miscellaneous";
import { AddressEdit, UserAddress } from "../../services/types/User";
import SectionDivider from "../core/SectionDivider";

interface AddressPopupProps {
  address: UserAddress | undefined;
  invtitationsEnbaled: boolean | undefined;
  onClose: () => void;
}

const defaultCords: MapCoordinates = {
  Lat: 0,
  Lng: 0,
};

const AddressPopup: React.FC<AddressPopupProps> = ({
  onClose,
  address,
  invtitationsEnbaled,
}) => {
  const [showMap, setShowMap] = useState(false);
  const [editAddress, setAdditableAddress] = useState<UserAddress>(address!);

  const [notificationEnabled, setNotificationEnabled] = useState<boolean>(
    invtitationsEnbaled!
  );

  const icon = L.icon({ iconUrl: marker, iconSize: [40, 40] });

  const [mapCoords, setMapCoords] = useState<MapCoordinates>(defaultCords);

  const [markerShow, setMarkerShow] = useState<boolean>(false);

  const LocationFinderDummy = () => {
    const map = useMapEvents({
      click(e) {
        if (e.latlng.lat !== 0 || e.latlng.lng !== 0) {
          const newCords: MapCoordinates = {
            Lat: e.latlng.lat,
            Lng: e.latlng.lng,
          };
          setMapCoords(newCords);
          handleChange("Map_X_Coords", mapCoords.Lat);
          handleChange("Map_Y_Coords", mapCoords.Lng);
          setMarkerShow(true);
        }
      },
    });
    return null;
  };

  const GetLatLng = (e: L.LatLng) => {
    const lating: MapCoordinates = {
      Lat: e.lat,
      Lng: e.lng,
    };
    setMapCoords(lating);
  };

  const handleChange = (
    name: keyof UserAddress,
    value: string | boolean | number | undefined
  ) => {
    setAdditableAddress((prevAddress) => ({
      ...prevAddress,
      [name]: value,
    }));
  };

  const postAddress = async (e: React.FormEvent<HTMLFormElement>) => {
    const loading = toast.loading("Updating address...");
    e.preventDefault();

    const newAddress: AddressEdit = {
      Address: editAddress,
      EnabledInvitationSettings: notificationEnabled,
    };
    console.log(newAddress);
    await updateAddress(newAddress);
    toast.success("Updated successfully", {
      id: loading,
    });
  };

  return (
    <div className="fixed inset-0 flex items-center justify-center z-50 bg-black bg-opacity-70">
      <div className="bg-white p-8 rounded-md w-96">
        <button
          onClick={onClose}
          className="absolute top-3 right-3 text-lg font-bold focus:outline-none "
        >
          &times;
        </button>
        <SectionDivider label="My Address" />
        <form onSubmit={postAddress}>
          <input
            type="text"
            placeholder="Country"
            onChange={(e) => handleChange("Country", e.target.value)}
            value={editAddress.Country}
            className="w-full mb-4 px-3 py-2 border rounded-md focus:outline-none focus:border-green-500 bg-white text-black"
          />
          <input
            type="text"
            value={editAddress.Province}
            onChange={(e) => handleChange("Province", e.target.value)}
            placeholder="Province"
            className="w-full mb-4 px-3 py-2 border rounded-md focus:outline-none focus:border-green-500 bg-white text-black"
          />
          <input
            type="text"
            onChange={(e) => handleChange("City", e.target.value)}
            value={editAddress.City}
            placeholder="City"
            className="w-full mb-4 px-3 py-2 border rounded-md focus:outline-none focus:border-green-500 bg-white text-black"
          />

          <input
            type="text"
            value={editAddress.StreetName}
            onChange={(e) => handleChange("StreetName", e.target.value)}
            placeholder="Street Name"
            className="w-full mb-4 px-3 py-2 border rounded-md focus:outline-none focus:border-green-500 bg-white text-black"
          />

          <label className="flex items-center cursor-pointer mb-4 text-black">
            <input
              type="checkbox"
              onChange={() => setShowMap(!showMap)}
              className="mr-2 cursor-pointer accent-green-500"
            />
            <span>Add your location in map</span>
          </label>
          {showMap && (
            <div className="h-64 w-full mb-4 border rounded-md">
              <MapContainer
                center={
                  editAddress.Map_X_Coords && editAddress.Map_Y_Coords
                    ? [editAddress.Map_X_Coords, editAddress.Map_Y_Coords]
                    : [54.90606490004955, 23.934473227312246]
                }
                zoom={13}
                scrollWheelZoom={false}
                style={{ height: "100%", minHeight: "100%" }}
              >
                <LocationFinderDummy />
                <TileLayer
                  attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                  url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                {(editAddress.Map_X_Coords !== 0 ||
                  editAddress.Map_Y_Coords !== 0) && (
                  <Marker
                    draggable={true}
                    icon={icon}
                    position={
                      editAddress.Map_X_Coords && editAddress.Map_Y_Coords
                        ? [editAddress.Map_X_Coords, editAddress.Map_Y_Coords]
                        : [0, 0]
                    }
                    eventHandlers={{
                      mouseup: (e) => GetLatLng(e.latlng),
                    }}
                  ></Marker>
                )}
              </MapContainer>
            </div>
          )}
          <label className="flex items-center cursor-pointer mb-4 text-black">
            <input
              type="checkbox"
              checked={notificationEnabled}
              onChange={() => setNotificationEnabled(!notificationEnabled)}
              className="bg-white mr-2 cursor-pointer checked:accent-green-500 custom-checkbox "
            />
            <span>Enable notifications for game invitations</span>
          </label>
          <button
            disabled={
              editAddress.Country === "" ||
              editAddress.Province === "" ||
              editAddress.City === "" ||
              editAddress.StreetName === ""
            }
            className="w-full px-4 py-2 text-white bg-green-500 rounded-md hover:bg-green-600 focus:outline-none disabled:bg-gray-500"
          >
            Submit
          </button>
          <button
            onClick={onClose}
            className="w-full px-4 py-2 text-white bg-red-500 mt-3 rounded-md hover:bg-red-600 focus:outline-none"
          >
            Discard changes
          </button>
        </form>
      </div>
    </div>
  );
};

export default AddressPopup;

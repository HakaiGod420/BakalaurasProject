import L from "leaflet";
import "leaflet/dist/leaflet.css";
import { useState } from "react";
import { MapContainer, Marker, TileLayer, useMapEvents } from "react-leaflet";
import { useWizard } from "react-use-wizard";
import marker from "../../assets/images/market.png";
import { MapCoordinates } from "../../services/types/Miscellaneous";
interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  mapCoords: MapCoordinates;
  setMapsCoords: React.Dispatch<React.SetStateAction<MapCoordinates>>;
}

function InviteStep3({
  stepNumber,
  setStepNumber,
  mapCoords,
  setMapsCoords,
}: Props) {
  const icon = L.icon({ iconUrl: marker, iconSize: [40, 40] });
  const { handleStep, previousStep, nextStep } = useWizard();

  const [markerShow, setMarkerShow] = useState<boolean>(false);

  const inputHandlerNext = () => {
    setStepNumber(stepNumber + 1);
    nextStep();
  };

  const inputHandlerPrevious = () => {
    setStepNumber(stepNumber - 1);
    previousStep();
  };

  const LocationFinderDummy = () => {
    const map = useMapEvents({
      click(e) {
        if (e.latlng.lat !== 0 || e.latlng.lng !== 0) {
          const newCords: MapCoordinates = {
            Lat: e.latlng.lat,
            Lng: e.latlng.lng,
          };
          console.log(newCords);
          setMapsCoords(newCords);
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
    setMapsCoords(lating);
  };

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className="text-center uppercase font-bold text-[20px]">
          Add Address where everything will be happening
        </h1>
        <p className="text-center p-2">
          To host your own table top event and invite other players to join you,
          please select a location on the map by clicking with your mouse. You
          can zoom in and out of the map to find the best spot for your event.
        </p>
        <div style={{ height: "500px" }} className=" rounded-lg">
          <MapContainer
            center={[54.90606490004955, 23.934473227312246]}
            zoom={13}
            scrollWheelZoom={false}
            style={{ height: "100%", minHeight: "100%" }}
          >
            <LocationFinderDummy />
            <TileLayer
              attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
              url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            {(mapCoords.Lat !== 0 || mapCoords.Lng !== 0) && (
              <Marker
                draggable={true}
                icon={icon}
                position={[mapCoords.Lat, mapCoords.Lng]}
                eventHandlers={{
                  mouseup: (e) => GetLatLng(e.latlng),
                }}
              ></Marker>
            )}
          </MapContainer>
        </div>
      </div>
      <div className="flex justify-center p-2 m-1">
        <button
          className="btn m-2 min-w-[10%]"
          onClick={() => inputHandlerPrevious()}
        >
          Previous
        </button>
        <button
          className="btn m-2 min-w-[100px]"
          onClick={() => inputHandlerNext()}
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default InviteStep3;

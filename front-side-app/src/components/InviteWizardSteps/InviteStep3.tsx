import L from "leaflet";
import "leaflet/dist/leaflet.css";
import { useState } from "react";
import { MapContainer, Marker, TileLayer, useMapEvents } from "react-leaflet";
import { useWizard } from "react-use-wizard";
import marker from "../../assets/images/market.png";
interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
}

interface Lating {
  Lat: number;
  Lng: number;
}
const defaultLating: Lating = {
  Lat: 0,
  Lng: 0,
};

function InviteStep3({ stepNumber, setStepNumber }: Props) {
  const icon = L.icon({ iconUrl: marker, iconSize: [40, 40] });
  const { handleStep, previousStep, nextStep } = useWizard();
  const [mapLtgLtd, setMapLtgLtd] = useState<Lating>(defaultLating);

  const [rules, setRules] = useState<string>("");
  const [markerShow, setMarkerShow] = useState<boolean>(false);

  const inputHandlerNext = () => {
    setStepNumber(stepNumber + 1);
    nextStep();
  };

  const inputHandlerSkip = () => {
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
          const newCords: Lating = {
            Lat: e.latlng.lat,
            Lng: e.latlng.lng,
          };
          console.log(newCords);
          setMapLtgLtd(newCords);
          setMarkerShow(true);
        }
      },
    });
    return null;
  };

  const GetLatLng = (e: L.LatLng) => {
    const lating: Lating = {
      Lat: e.lat,
      Lng: e.lng,
    };
    setMapLtgLtd(lating);
  };

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className="text-center uppercase font-bold text-[20px]">
          Add Address where everything will be happening
        </h1>
        <p className="text-center p-2">
          Write the rules of the tabletop game you are creating now. You can
          skip this step, but others may not know how to play your game.
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
            {markerShow && (
              <Marker
                draggable={true}
                icon={icon}
                position={[mapLtgLtd.Lat, mapLtgLtd.Lng]}
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

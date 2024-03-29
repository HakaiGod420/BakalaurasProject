import { Collapse, Space } from "antd";
import L from "leaflet";
import "leaflet/dist/leaflet.css";
import { useEffect, useRef, useState } from "react";
import { MapContainer, Marker, TileLayer, useMapEvents } from "react-leaflet";
import { useWizard } from "react-use-wizard";
import marker from "../../assets/images/market.png";
import { Countries } from "../../constants/Countries";
import { Address } from "../../services/types/Invitation";
import { MapCoordinates } from "../../services/types/Miscellaneous";
interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  mapCoords: MapCoordinates;
  setMapsCoords: React.Dispatch<React.SetStateAction<MapCoordinates>>;
  address: Address;
  setAddress: React.Dispatch<React.SetStateAction<Address>>;
}

function InviteStep3({
  stepNumber,
  setStepNumber,
  mapCoords,
  setMapsCoords,
  address,
  setAddress,
}: Props) {
  const { Panel } = Collapse;
  const icon = L.icon({ iconUrl: marker, iconSize: [40, 40] });
  const { previousStep, nextStep } = useWizard();

  const [, setMarkerShow] = useState<boolean>(false);

  const inputHandlerNext = () => {
    setStepNumber(stepNumber + 1);
    nextStep();
  };

  const handleChange = (name: string, value: string | undefined) => {
    setAddress({
      ...address,
      [name]: value,
    });
  };

  const inputHandlerPrevious = () => {
    setStepNumber(stepNumber - 1);
    previousStep();
  };

  const LocationFinderDummy = () => {
    useMapEvents({
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

  const [searchTerm, setSearchTerm] = useState("");
  const [isOpen, setIsOpen] = useState(false);

  const filteredCountries = Countries.filter((country) =>
    country.toLowerCase().startsWith(searchTerm.toLowerCase())
  );

  const handleSelect = (event: any) => {
    handleChange("Country", event.target.outerText);
    setSearchTerm(event.target.outerText);
    setIsOpen(false);
  };

  const handleInput = (event: any) => {
    setSearchTerm(event.target.value);
    setIsOpen(true);
  };

  const ref = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (ref.current && !ref.current.contains(event.target as Node)) {
        setIsOpen(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);

    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [ref]);

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className="text-center uppercase font-bold text-[20px]">
          Add the address where the event will take place.
        </h1>
        <p className="text-center p-2">
          To host your own table top event and invite other players to join you,
          please select a location on the map by clicking with your mouse. You
          can zoom in and out of the map to find the best spot for your event.
        </p>

        <Collapse className="mt-5" defaultActiveKey={["0"]}>
          <Panel
            header={
              <p className="text-[#bad0ef] font-bold">
                Input full address of meet
              </p>
            }
            key="1"
          >
            <Space wrap className="flex justify-left">
              <div className=" font-bold flex justify-center w-[200px] md:w-[400px] lg:w-[400px] xl:w-[200px] ">
                <p className="text-[#bad0ef] font-bold text-[15px]">Country</p>
              </div>
              <div className="p-2 flex justify-center relative" ref={ref}>
                <div className="relative w-full max-w-xs">
                  <input
                    type="text"
                    placeholder="Search for a country"
                    className="input input-bordered input-success w-full max-w-xs text-[#bad0ef] z-10 rounded-md"
                    value={searchTerm}
                    onChange={handleInput}
                    onFocus={() => setIsOpen(true)}
                  />
                  {isOpen && (
                    <div className="absolute top-full left-0 w-full z-20">
                      <ul className="list-none p-0 m-0 rounded-md bg-gray-700 text-white overflow-y-scroll max-h-32">
                        {filteredCountries.slice(0, 5).map((country) => (
                          <li
                            key={country}
                            className="px-3 py-2 hover:bg-gray-600 cursor-pointer"
                            onClick={handleSelect}
                            value={country}
                          >
                            {country}
                          </li>
                        ))}
                      </ul>
                    </div>
                  )}
                </div>
              </div>

              <div className=" font-bold flex justify-center w-[200px] md:w-[400px] lg:w-[400px] xl:w-[200px] ">
                <p className="text-[#bad0ef] font-bold text-[15px]">City</p>
              </div>
              <div className="p-2 flex justify-center">
                <input
                  onChange={(e) => handleChange("City", e.target.value)}
                  value={address.City}
                  type="text"
                  className="input input-bordered input-success w-full max-w-xs text-[#bad0ef]"
                />
              </div>
              <div className=" font-bold flex justify-center w-[200px] md:w-[400px] lg:w-[400px] xl:w-[200px]">
                <p className="text-[#bad0ef] font-bold text-[15px]">Province</p>
              </div>
              <div className="p-2 flex justify-center">
                <input
                  onChange={(e) => handleChange("Province", e.target.value)}
                  value={address.Province}
                  type="text"
                  className="input input-bordered input-success w-full max-w-xs text-[#bad0ef]"
                />
              </div>
              <div className=" font-bold flex justify-center w-[200px] md:w-[400px] lg:w-[400px] xl:w-[200px]">
                <p className="text-[#bad0ef] font-bold text-[15px]">
                  Street name
                </p>
              </div>
              <div className="p-2 flex justify-center">
                <input
                  onChange={(e) => handleChange("StreetName", e.target.value)}
                  value={address.StreetName}
                  type="text"
                  className="input input-bordered input-success w-full max-w-xs text-[#bad0ef]"
                />
              </div>
              <div className=" font-bold flex justify-center w-[200px] md:w-[400px] lg:w-[400px] xl:w-[200px]">
                <p className="text-[#bad0ef] font-bold text-[15px]">
                  House number
                </p>
              </div>
              <div className="p-2 flex justify-center">
                <input
                  onChange={(e) =>
                    handleChange("HouseNumber", e.target.value || undefined)
                  }
                  value={address.HouseNumber}
                  type="number"
                  className="input input-bordered input-success w-full max-w-xs text-[#bad0ef]"
                />
              </div>
            </Space>
          </Panel>
          <Panel
            header={
              <p className="text-[#bad0ef] font-bold">
                Select place on the map
              </p>
            }
            key="2"
          >
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
          </Panel>
        </Collapse>
      </div>
      <div className="flex justify-center p-2 m-1">
        <button
          className="btn m-2 min-w-[10%]"
          onClick={() => inputHandlerPrevious()}
        >
          Previous
        </button>
        <button
          disabled={
            address.City === "" ||
            address.Province === "" ||
            address.StreetName === "" ||
            address.HouseNumber === undefined ||
            mapCoords.Lat === 0 ||
            mapCoords.Lng === 0
          }
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

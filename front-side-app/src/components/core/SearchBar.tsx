import { useState } from "react";
import { AiOutlineSearch } from "react-icons/ai";
import { useNavigate } from "react-router-dom";

const SearchBar = () => {
  const navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState("");

  const handleSearch = () => {
    navigate("/gameboards?searchTerm=" + searchTerm);
  };

  const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === "Enter") {
      handleSearch();
    }
  };
  return (
    <div>
      <div className="relative">
        <button
          onClick={handleSearch}
          className="absolute inset-y-0 right-0 px-4 text-gray-400 hover:text-gray-600"
        >
          <AiOutlineSearch size={24} />
        </button>
        <input
          type="text"
          placeholder="Search"
          className="block w-full px-4 py-2 text-gray-200 placeholder-gray-400 border border-gray-600 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-500 focus:border-transparent bg-gray-900"
          value={searchTerm}
          onChange={(event) => setSearchTerm(event.target.value)}
          onKeyDown={handleKeyPress}
        />
      </div>
    </div>
  );
};

export default SearchBar;

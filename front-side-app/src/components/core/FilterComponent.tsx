import { useEffect, useState } from "react";
import { FaSearch, FaTimes } from "react-icons/fa";
import Select from "react-select";
import { getGameBoardTypesAndCategories } from "../../services/api/GameBoardService";
import {
  CategoryOptions,
  Filter,
  TypeOptions,
} from "../../services/types/TabletopGame";

interface FilterProps {
  //onFilterChange: (filters: Filters) => void;
}

interface Filters {
  title: string;
  rating: number | null;
  creationDate: Date | null;
  categories: string[];
  types: string[];
}

const FilterComponent: React.FC<FilterProps> = () => {
  const [types, setTypes] = useState<TypeOptions[]>([]);
  const [categories, setCategories] = useState<CategoryOptions[]>([]);

  const [filters, setFilters] = useState<Filter>({
    title: "",
    rating: "",
    creationDate: null,
    categories: [],
    types: [],
  });
  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    console.log("handleInputChange called with value:", e.target.value);
    setFilters({ ...filters, title: e.target.value });
  };

  const handleRatingChange = (value: any) => {
    console.log("handleRatingChange called with value:", value);
    const rating = value ? value.label : null;
    setFilters({ ...filters, rating });
  };

  const handleDateChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    console.log("handleDateChange called with value:", e.target.value);
    const date = e.target.value ? new Date(e.target.value) : null;
    setFilters({ ...filters, creationDate: date });
  };

  const handleCategoryChange = (values: any) => {
    console.log("handleCategoryChange called with values:", values);
    const categories = values.map((value: any) => ({
      value: value.value,
      label: value.label,
    }));
    setFilters({ ...filters, categories });
  };

  const handleTypeChange = (values: any) => {
    console.log("handleTypeChange called with values:", values);
    const types = values.map((value: any) => ({
      value: value.value,
      label: value.label,
    }));
    setFilters({ ...filters, types });
  };

  console.log(types);
  const handleClearFilters = () => {
    console.log("handleClearFilters called");
    setFilters({
      title: "",
      rating: "",
      creationDate: null,
      categories: [],
      types: [],
    });

    /*
    onFilterChange({
      title: "",
      rating: [],
      creationDate: null,
      categories: [],
      types: [],
    });*/
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log("handleSubmit called with filters:", filters);
    //onFilterChange(filters);
  };

  useEffect(() => {
    const fetchData = async () => {
      const response = await getGameBoardTypesAndCategories();
      if (response !== undefined) {
        const typesGet = response.Types.map((element) => ({
          value: element.Value,
          label: element.Label,
        }));
        setTypes(typesGet);

        const categoriesGet = response.Categories.map((element) => ({
          value: element.Value,
          label: element.Label,
        }));
        setCategories(categoriesGet);
      }
    };

    fetchData();

    console.log("useEffect called with types:", types);
  }, [setTypes, setCategories]);

  return (
    <form
      onSubmit={handleSubmit}
      className="bg-gray-700 rounded-md p-4 md:flex md:flex-wrap md:items-center"
    >
      <div className="w-full md:w-1/3 p-2">
        <label htmlFor="title" className="sr-only">
          Title
        </label>
        <input
          type="text"
          id="title"
          value={filters.title}
          onChange={handleInputChange}
          placeholder="Title"
          className="block w-full bg-gray-800 border border-gray-600 rounded-lg py-3 px-4 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
        />
      </div>
      <div className="w-full md:w-1/3 p-2">
        <label htmlFor="creationDate" className="sr-only">
          Creation Date
        </label>
        <input
          type="date"
          id="creationDate"
          value={
            filters.creationDate
              ? filters.creationDate.toISOString().slice(0, 10)
              : ""
          }
          onChange={handleDateChange}
          placeholder="Creation Date"
          className="block w-full placeholder:text-black bg-gray-800 border border-gray-600 rounded-lg py-3 px-4 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
        />
      </div>
      <div className="w-full md:w-1/3 p-2">
        <label htmlFor="rating" className="sr-only">
          Rating
        </label>
        <Select
          id="rating"
          value={
            filters.rating === ""
              ? null
              : { value: filters.rating, label: filters.rating }
          }
          options={[
            { value: "1", label: "0-1" },
            { value: "2", label: "1-2" },
            { value: "3", label: "2-3" },
            { value: "4", label: "3-4" },
            { value: "5", label: "4-5" },
          ]}
          onChange={handleRatingChange}
          placeholder="Rating"
          className="w-full"
          styles={{
            control: (provided) => ({
              ...provided,
              backgroundColor: "#1f2937",
              borderRadius: "0.5rem",
              border: "1px solid #4B5563",
              color: "white",
              minHeight: "3rem",
            }),
            option: (provided, state) =>
              Object.assign({}, provided, {
                color: "black",
              }),

            singleValue: (provided) => ({
              ...provided,
              color: "white",
            }),
          }}
        />
      </div>
      <div className="w-full md:w-1/2 p-2">
        <label htmlFor="categories" className="sr-only">
          Categories
        </label>
        <Select
          id="categories"
          options={categories}
          isMulti
          isClearable
          value={filters.categories}
          onChange={handleCategoryChange}
          placeholder="Categories"
          className="w-full"
          styles={{
            control: (provided) => ({
              ...provided,
              backgroundColor: "#1f2937",
              borderRadius: "0.5rem",
              border: "1px solid #4B5563",
              color: "white",
              minHeight: "3rem",
            }),
            option: (provided, state) =>
              Object.assign({}, provided, {
                backgroundColor: state.isSelected ? "#1D4ED8" : null,
                color: "black",
              }),
            input: (provided) => ({
              ...provided,
              color: "white",
            }),
          }}
        />
      </div>
      <div className="w-full md:w-1/2 p-2">
        <label htmlFor="types" className="sr-only">
          Types
        </label>
        <Select
          id="types"
          options={types}
          isMulti
          onChange={handleTypeChange}
          placeholder="Types"
          className="w-full"
          value={filters.types.length === 0 ? null : filters.types}
          isClearable
          styles={{
            control: (provided) => ({
              ...provided,
              backgroundColor: "#1f2937",
              borderRadius: "0.5rem",
              border: "1px solid #4B5563",
              color: "white",
              minHeight: "3rem",
            }),
            option: (provided, state) =>
              Object.assign({}, provided, {
                backgroundColor: state.isSelected ? "#1D4ED8" : null,
                color: "black",
              }),
            input: (provided) => ({
              ...provided,
              color: "white",
            }),
          }}
        />
      </div>
      <div className="w-full flex flex-col md:flex-row justify-between p-0 mt-5">
        <button
          type="submit"
          className="flex items-center justify-center bg-blue-500 hover:bg-blue-700 text-white font-bold py-3 px-6 rounded-lg md:ml-2 md:mt-0 md:w-full"
        >
          <FaSearch className="mr-2" />
          <span>Filter</span>
        </button>
        <button
          type="button"
          onClick={handleClearFilters}
          className="flex items-center justify-center bg-gray-500 hover:bg-gray-400 text-white font-bold py-3 px-6 rounded-lg md:ml-2 md:mt-0 md:w-full"
        >
          <FaTimes className="mr-2" />
          <span>Clear Filters</span>
        </button>
      </div>
    </form>
  );
};

export default FilterComponent;

import React, { useState } from "react";
import { IoMdClose } from "react-icons/io";

interface Category {
  id: number;
  name: string;
}

interface Type {
  id: number;
  name: string;
}

const categories: Category[] = [
  { id: 1, name: "Category 1" },
  { id: 2, name: "Category 2" },
  { id: 3, name: "Category 3" },
];

const types: Type[] = [
  { id: 1, name: "Type 1" },
  { id: 2, name: "Type 2" },
  { id: 3, name: "Type 3" },
];

const FilterComponent: React.FC = () => {
  const [selectedCategories, setSelectedCategories] = useState<Category[]>([]);
  const [selectedTypes, setSelectedTypes] = useState<Type[]>([]);
  const [title, setTitle] = useState("");
  const [creationDate, setCreationDate] = useState("");
  const [rating, setRating] = useState("");

  const handleCategorySelect = (category: Category) => {
    setSelectedCategories((prevSelectedCategories) =>
      prevSelectedCategories.includes(category)
        ? prevSelectedCategories.filter((c) => c !== category)
        : [...prevSelectedCategories, category]
    );
  };

  const handleTypeSelect = (type: Type) => {
    setSelectedTypes((prevSelectedTypes) =>
      prevSelectedTypes.includes(type)
        ? prevSelectedTypes.filter((t) => t !== type)
        : [...prevSelectedTypes, type]
    );
  };

  const handleAddCategoryTag = (category: Category) => {
    if (!selectedCategories.includes(category)) {
      setSelectedCategories([...selectedCategories, category]);
    }
  };

  const handleAddTypeTag = (type: Type) => {
    if (!selectedTypes.includes(type)) {
      setSelectedTypes([...selectedTypes, type]);
    }
  };

  const handleRemoveCategoryTag = (category: Category) => {
    setSelectedCategories((prevSelectedCategories) =>
      prevSelectedCategories.filter((c) => c !== category)
    );
  };

  const handleRemoveTypeTag = (type: Type) => {
    setSelectedTypes((prevSelectedTypes) =>
      prevSelectedTypes.filter((t) => t !== type)
    );
  };

  return (
    <div className="w-full bg-white dark:bg-gray-200 p-3 rounded-md">
      <div className="grid grid-cols-2 gap-4 md:grid-cols-3">
        {/* Categories */}
        <div>
          <label
            htmlFor="
categories"
            className="block text-gray-700 font-bold mb-2"
          >
            Categories
          </label>
          <div className="mt-2">
            {selectedCategories.map((category) => (
              <span
                key={category.id}
                className="inline-flex items-center px-3 py-1 rounded-full text-sm font-medium bg-blue-100 text-blue-800 mr-2 mb-2"
              >
                {category.name}
                <button
                  type="button"
                  onClick={() => handleRemoveCategoryTag(category)}
                  className="flex-shrink-0 ml-1 focus:outline-none"
                >
                  <IoMdClose className="h-4 w-4" />
                </button>
              </span>
            ))}
          </div>
          <div className="relative">
            <select
              id="categories"
              multiple
              className="block w-full rounded-md border-gray-300 shadow-sm py-2 px-3 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
              onChange={(e) =>
                handleCategorySelect(
                  categories.find((c) => c.id === Number(e.target.value))!
                )
              }
            >
              {categories.map((category) => (
                <option key={category.id} value={category.id}>
                  {category.name}
                </option>
              ))}
            </select>
          </div>
        </div>
        <div>
          <label htmlFor="types" className="block text-gray-700 font-bold mb-2">
            Types
          </label>
          <div className="mt-2">
            {selectedTypes.map((type) => (
              <span
                key={type.id}
                className="inline-flex items-center px-3 py-1 rounded-full text-sm font-medium bg-blue-100 text-blue-800 mr-2 mb-2"
              >
                {type.name}
                <button
                  type="button"
                  onClick={() => handleRemoveTypeTag(type)}
                  className="flex-shrink-0 ml-1 focus:outline-none"
                >
                  <IoMdClose className="h-4 w-4" />
                </button>
              </span>
            ))}
          </div>
          <div className="relative">
            <select
              id="types"
              multiple
              className="block w-full rounded-md border-gray-300 shadow-sm py-2 px-3 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
              onChange={(e) =>
                handleTypeSelect(
                  types.find((t) => t.id === Number(e.target.value))!
                )
              }
            >
              {types.map((type) => (
                <option key={type.id} value={type.id}>
                  {type.name}
                </option>
              ))}
            </select>
          </div>
        </div>

        {/* Title */}
        <div>
          <label htmlFor="title" className="block text-gray-700 font-bold mb-2">
            Title
          </label>
          <input
            id="title"
            type="text"
            className="block w-full rounded-md border-gray-300 shadow-sm py-2 px-3 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
          />
        </div>

        {/* Creation Date */}
        <div>
          <label
            htmlFor="creationDate"
            className="block text-gray-700 font-bold mb-2"
          >
            Creation Date
          </label>
          <input
            id="creationDate"
            type="date"
            className="block w-full rounded-md border-gray-300 shadow-sm py-2 px-3 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
            value={creationDate}
            onChange={(e) => setCreationDate(e.target.value)}
          />
        </div>

        {/* Rating */}
        <div>
          <label
            htmlFor="rating"
            className="block text-gray-700 font-bold mb-2"
          >
            Rating
          </label>
          <select
            id="rating"
            className="block w-full rounded-md border-gray-300 shadow-sm py-2 px-3 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
            value={rating}
            onChange={(e) => setRating(e.target.value)}
          >
            <option value="">Select rating</option>
            <option value="0-1">0-1</option>
            <option value="0-2">0-2</option>
            <option value="0-3">0-3</option>
            <option value="0-4">0-4</option>
            <option value="0-5">0-5</option>
          </select>
        </div>
      </div>
    </div>
  );
};

export default FilterComponent;

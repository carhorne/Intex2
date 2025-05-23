import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Movie } from "../types/Movie";
import { moviePosters as allPosters } from "../data/moviePosters";
import MovieModal from "../components/MovieModel";
import "../components/GenrePage.css";

const getPosterUrl = (title: string | undefined): string => {
  if (!title) return "/posters/default.jpg";

  const sanitize = (str: string) =>
    str.normalize("NFD").replace(/[^a-zA-Z0-9 ]/g, "").trim();

  const sanitizedTitle = sanitize(title);
  const encodedTitle = encodeURIComponent(sanitizedTitle);
  const match = allPosters.find((path) => path === `/posters/${encodedTitle}.jpg`);

  const baseUrl = "https://movieblob4logang.blob.core.windows.net/posters";
  return match ? `${baseUrl}${match}` : "/posters/default.jpg";
};

const GenreDisplay = () => {
  const { genre: rawGenre } = useParams<{ genre: string }>();
  const decodedGenre = decodeURIComponent(rawGenre ?? "");
  console.log("Decoded Genre: ", decodedGenre); // Debugging line

  const [movies, setMovies] = useState<Movie[]>([]);
  const [selectedMovieId, setSelectedMovieId] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchMoviesByGenre = async () => {
      try {
        const token = localStorage.getItem("authToken");
        const response = await fetch(
          `https://intex-backend-4logan-g8agdge9hsc2aqep.westus-01.azurewebsites.net/api/movies/by-genre`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
              "Content-Type": "application/json",
            },
          }
        );
    
        const data = await response.json();
        console.log("Fetched Data:", data); // Debugging line
    
        // Normalize the genre and check for matches
        const normalizeGenre = (str: string) =>
          str
            .toLowerCase()                // Convert to lowercase
            .replace(/[^a-z0-9\s]/g, "")   // Remove non-alphanumeric characters (except spaces)
            .replace(/\s+/g, "%20")        // Replace spaces with %20 (to match URL encoding)
            .trim();                       // Trim any leading or trailing spaces
    
        // Normalize the decoded genre from the URL (frontend)
        const normalizedDecodedGenre = normalizeGenre(decodedGenre);
        console.log("Normalized Decoded Genre:", normalizedDecodedGenre); // Debugging line
    
        // Find the matching genre key
        const matchingGenreKey = Object.keys(data).find(
          (key) => normalizeGenre(key) === normalizedDecodedGenre
        );
        console.log("Matching Genre Key:", matchingGenreKey); // Debugging line
    
        const genreMovies = matchingGenreKey ? data[matchingGenreKey] : [];
        setMovies(genreMovies);  // Set the movies to state
      } catch (error) {
        console.error("Error fetching movies by genre:", error);
      }
    };
    
    fetchMoviesByGenre();
  }, [decodedGenre]);
  
  


  const handlePosterClick = (movie: Movie) => {
    setSelectedMovieId(movie.showId ?? null);
  };

  return (
    <div className="genre-display-page">
      <button className="back-btn" onClick={() => navigate("/home")}>
        ← Back to Home
      </button>
      <h1 className="genre-heading">{decodedGenre}</h1>
      <div className="genre-movie-grid">
        {movies.map((movie) => (
          <div key={movie.showId} className="movie-card">
            <img
              src={getPosterUrl(movie.title)}
              alt={movie.title ?? "Movie poster"}
              className="movie-poster"
              onClick={() => handlePosterClick(movie)}
              onError={(e) => (e.currentTarget.src = "/posters/default.jpg")}
            />
            <h3>{movie.title}</h3>
            <p className="movie-meta">
              {(movie.releaseYear ?? "") +
                (movie.releaseYear && movie.country ? " • " : "") +
                (movie.country ?? "")}
            </p>
          </div>
        ))}
      </div>

      {selectedMovieId && (
        <MovieModal
          movieId={selectedMovieId}
          onClose={() => setSelectedMovieId(null)}
        />
      )}
    </div>
  );
};

export default GenreDisplay;
